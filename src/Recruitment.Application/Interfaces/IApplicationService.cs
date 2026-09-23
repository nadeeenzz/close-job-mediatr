using Recruitment.Application.Dtos;
using Recruitment.Domain.Enums;

namespace Recruitment.Application.Interfaces;

public interface IApplicationService
{
    ApplicationResponse Apply(Guid jobId, Guid candidateId);
    ApplicationResponse Cancel(Guid applicationId);
    ApplicationResponse UpdateStatus(Guid applicationId, ApplicationStatus newStatus);
    ApplicationResponse GetById(Guid applicationId);
    List<ApplicationResponse> GetAll();
}
