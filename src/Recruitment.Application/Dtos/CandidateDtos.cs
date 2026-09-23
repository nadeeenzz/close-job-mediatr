namespace Recruitment.Application.Dtos;

public record RegisterCandidateRequest(string FullName, string Email, string? Phone);

public record UpdateCandidateProfileRequest(string FullName, string? Phone);

public record CandidateResponse(Guid Id, string FullName, string Email, string? Phone, DateTime CreatedAt);
