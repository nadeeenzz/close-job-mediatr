using Recruitment.Domain.Enums;

namespace Recruitment.Application.Dtos;

public record ApplyRequest(Guid JobId, Guid CandidateId);

public record UpdateApplicationStatusRequest(ApplicationStatus Status);

public record ApplicationResponse(Guid Id, Guid JobId, Guid CandidateId, ApplicationStatus Status, DateTime AppliedAt);
