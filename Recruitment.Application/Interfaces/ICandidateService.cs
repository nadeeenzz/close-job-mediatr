using Recruitment.Application.Dtos;

namespace Recruitment.Application.Interfaces;

public interface ICandidateService
{
    CandidateResponse Register(string fullName, string email, string? phone);
    CandidateResponse UpdateProfile(Guid candidateId, string fullName, string? phone);
    CandidateResponse GetById(Guid candidateId);
    List<CandidateResponse> GetAll();
}
