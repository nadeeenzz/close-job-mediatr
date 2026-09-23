using Recruitment.Application.Dtos;

namespace Recruitment.Application.Interfaces;

public interface IJobService
{
    JobResponse Create(string title, string description);
    JobResponse Close(Guid jobId);
    JobResponse Reopen(Guid jobId);
    JobResponse GetById(Guid jobId);
    List<JobResponse> GetAll();
}
