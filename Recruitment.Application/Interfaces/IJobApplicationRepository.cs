using Recruitment.Domain.Entities;

namespace Recruitment.Application.Interfaces;

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    // Used to stop a candidate applying twice to the same job
    bool HasActiveApplication(Guid jobId, Guid candidateId);
}
