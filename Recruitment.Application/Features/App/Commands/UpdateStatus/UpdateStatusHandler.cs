using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Enums;
using JobApplicationEntity = Recruitment.Domain.Entities.JobApplication;

namespace Recruitment.Application.Features.App.Commands.UpdateStatus;

public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, ApplicationResponse>
{
    private readonly IJobApplicationRepository _applications;

    public UpdateStatusHandler(IJobApplicationRepository applications)
    {
        _applications = applications;
    }

    public Task<ApplicationResponse> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
       
        var application = _applications.GetById(request.ApplicationId)
            ?? throw new NotFoundException($"Job Application {request.ApplicationId} was not found.");

        if (application.Status == ApplicationStatus.Cancelled)
            throw new BusinessRuleException("A cancelled application cannot be updated.");

        if (request.Status == ApplicationStatus.Cancelled)
            throw new BusinessRuleException("Use the cancel action to cancel an application.");

        application.Status = request.Status;
        _applications.Update(application);

        return Task.FromResult(ToResponse(application));
    }
    private static ApplicationResponse ToResponse(JobApplicationEntity application)
    {
        return new ApplicationResponse(
            application.Id,
            application.JobId,
            application.CandidateId,
            application.Status,
            application.AppliedAt
        );
    }
}