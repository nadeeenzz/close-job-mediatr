using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Domain.Enums;

namespace Recruitment.Application.Services;

// One service for the Job entity: Close(), Reopen() + a few basics.
public class JobService : IJobService
{
    private readonly IRepository<Job> _jobs;

    public JobService(IRepository<Job> jobs)
    {
        _jobs = jobs;
    }

    public JobResponse Create(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new BusinessRuleException("Job title is required.");

        var job = new Job { Title = title.Trim(), Description = description?.Trim() ?? "" };
        _jobs.Add(job);
        return ToResponse(job);
    }

    public JobResponse Close(Guid jobId)
    {
        var job = FindJob(jobId);

        if (job.Status == JobStatus.Closed)
            throw new BusinessRuleException("Job is already closed.");

        job.Status = JobStatus.Closed;
        _jobs.Update(job);
        return ToResponse(job);
    }

    public JobResponse Reopen(Guid jobId)
    {
        var job = FindJob(jobId);

        if (job.Status == JobStatus.Open)
            throw new BusinessRuleException("Job is already open.");

        job.Status = JobStatus.Open;
        _jobs.Update(job);
        return ToResponse(job);
    }

    public JobResponse GetById(Guid jobId) => ToResponse(FindJob(jobId));

    public List<JobResponse> GetAll() => _jobs.GetAll().Select(ToResponse).ToList();

    private Job FindJob(Guid jobId)
    {
        return _jobs.GetById(jobId)
               ?? throw new NotFoundException($"Job {jobId} was not found.");
    }

    private static JobResponse ToResponse(Job j) =>
        new(j.Id, j.Title, j.Description, j.Status, j.CreatedAt);
}
