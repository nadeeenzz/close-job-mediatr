using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Application.Services;
using JobEntity = Recruitment.Domain.Entities.Job; 
namespace Recruitment.Application.Features.Job.Commands.Create;

public class CreateHandler : IRequestHandler<CreateCommand, JobResponse>
{
    private readonly IJobService _jobs; 

    public CreateHandler(IJobService jobs)
    {
        _jobs = jobs;
    }

    public Task<JobResponse> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new BusinessRuleException("Job title is required.");

        var job = new JobEntity
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim() ?? string.Empty
        };

        var response = _jobs.Create(request.Title, request.Description);

        return Task.FromResult(ToResponse(job));
    }

    private static JobResponse ToResponse(JobEntity job)
    {
        return new JobResponse(
            job.Id,
            job.Title,
            job.Description,
            job.Status,
            job.CreatedAt
        );
    }
}