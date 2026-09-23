using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Domain.Enums;

namespace Recruitment.Application.Features.App.Commands.CancleJob;

public class CancleJobHandler : IRequestHandler<CancelApplicationCommand, ApplicationResponse>
{
    private readonly IJobApplicationRepository _applications;

    public CancleJobHandler(IJobApplicationRepository applications)
    {
        _applications = applications;
    }

    public Task<ApplicationResponse> Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
    {
        // استخدام request.ApplicationId بدلاً من request.Id
        var application = _applications.GetById(request.ApplicationId);

        if (application == null)
        {
            throw new NotFoundException($"Job Application {request.ApplicationId} was not found.");
        }

        if (application.Status is ApplicationStatus.Accepted
            or ApplicationStatus.Rejected
            or ApplicationStatus.Cancelled)
        {
            throw new BusinessRuleException($"An application that is {application.Status} cannot be cancelled.");
        }

        application.Status = ApplicationStatus.Cancelled;

        _applications.Update(application);

        return Task.FromResult(ToResponse(application));
    }

private static ApplicationResponse ToResponse(Recruitment.Domain.Entities.JobApplication application)
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