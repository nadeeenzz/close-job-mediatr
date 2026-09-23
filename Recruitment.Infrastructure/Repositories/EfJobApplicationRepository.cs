using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Domain.Enums;
using Recruitment.Infrastructure.Data;

namespace Recruitment.Infrastructure.Repositories;

public class EfJobApplicationRepository : EfRepository<JobApplication>, IJobApplicationRepository
{
    public EfJobApplicationRepository(AppDbContext db) : base(db)
    {
    }

    public bool HasActiveApplication(Guid jobId, Guid candidateId)
    {
        return Db.JobApplications.Any(a =>
            a.JobId == jobId &&
            a.CandidateId == candidateId &&
            a.Status != ApplicationStatus.Cancelled);
    }
}
