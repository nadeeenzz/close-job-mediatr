using Recruitment.Domain.Enums;

namespace Recruitment.Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid JobId { get; set; }
    public Guid CandidateId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
}
