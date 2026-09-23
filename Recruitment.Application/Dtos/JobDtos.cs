using Recruitment.Domain.Enums;

namespace Recruitment.Application.Dtos;

public record CreateJobRequest(string Title, string Description);

public record JobResponse(Guid Id, string Title, string Description, JobStatus Status, DateTime CreatedAt);
