using Recruitment.Domain.Enums;

namespace Recruitment.Domain.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public JobStatus Status { get; set; } = JobStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
