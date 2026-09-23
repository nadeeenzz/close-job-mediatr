using MediatR;
using Recruitment.Application.Dtos;

namespace JobApplication.Application.Features.Applications.Commands.CancelApplication;

public class CancelApplicationCommand : IRequest<ApplicationResponse>
{
    public Guid ApplicationId { get; set; }
    public string Userid { get; set; }

    public CancelApplicationCommand(Guid applicationId, string userId)
    {
        ApplicationId = applicationId;
        Userid = userId;
    }
}