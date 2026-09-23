using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Domain.Enums;

namespace Recruitment.Application.Features.App.Commands.UpdateStatus;

public class UpdateStatusCommand : IRequest<ApplicationResponse>
{
    public Guid ApplicationId { get; set; }
    public ApplicationStatus Status { get; set; }

    public UpdateStatusCommand(Guid applicationId, ApplicationStatus status)
    {
        ApplicationId = applicationId;
        Status = status;
    }
}