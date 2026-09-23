using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Domain.Enums;

namespace Recruitment.Application.Services;

// One service for the Application entity: Apply(), Cancel(), UpdateStatus() + basics.
public class ApplicationService : IApplicationService
{
    private readonly IJobApplicationRepository _applications;
    private readonly IRepository<Job> _jobs;
    private readonly IRepository<Candidate> _candidates;

    public ApplicationService(
        IJobApplicationRepository applications,
        IRepository<Job> jobs,
        IRepository<Candidate> candidates)
    {
        _applications = applications;
        _jobs = jobs;
        _candidates = candidates;
    }

    public ApplicationResponse Apply(Guid jobId, Guid candidateId)
    {
        var job = _jobs.GetById(jobId)
                  ?? throw new NotFoundException($"Job {jobId} was not found.");

        if (_candidates.GetById(candidateId) is null)
            throw new NotFoundException($"Candidate {candidateId} was not found.");

        if (job.Status == JobStatus.Closed)
            throw new BusinessRuleException("You cannot apply to a closed job.");

        if (_applications.HasActiveApplication(jobId, candidateId))
            throw new BusinessRuleException("Candidate already applied to this job.");

        var application = new JobApplication { JobId = jobId, CandidateId = candidateId };
        _applications.Add(application);
        return ToResponse(application);
    }

    public ApplicationResponse Cancel(Guid applicationId)
    {
        var application = FindApplication(applicationId);

        if (application.Status is ApplicationStatus.Accepted
                               or ApplicationStatus.Rejected
                               or ApplicationStatus.Cancelled)
            throw new BusinessRuleException(
                $"An application that is {application.Status} cannot be cancelled.");

        application.Status = ApplicationStatus.Cancelled;
        _applications.Update(application);
        return ToResponse(application);
    }

    public ApplicationResponse UpdateStatus(Guid applicationId, ApplicationStatus newStatus)
    {
        var application = FindApplication(applicationId);

        if (application.Status == ApplicationStatus.Cancelled)
            throw new BusinessRuleException("A cancelled application cannot be updated.");

        if (newStatus == ApplicationStatus.Cancelled)
            throw new BusinessRuleException("Use the cancel action to cancel an application.");

        application.Status = newStatus;
        _applications.Update(application);
        return ToResponse(application);
    }

    public ApplicationResponse GetById(Guid applicationId) => ToResponse(FindApplication(applicationId));

    public List<ApplicationResponse> GetAll() => _applications.GetAll().Select(ToResponse).ToList();

    private JobApplication FindApplication(Guid applicationId)
    {
        return _applications.GetById(applicationId)
               ?? throw new NotFoundException($"Application {applicationId} was not found.");
    }

    private static ApplicationResponse ToResponse(JobApplication a) =>
        new(a.Id, a.JobId, a.CandidateId, a.Status, a.AppliedAt);
}
