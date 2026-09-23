using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;

namespace Recruitment.Application.Services;

// One service for the Candidate entity: Register(), UpdateProfile() + basics.
public class CandidateService : ICandidateService
{
    private readonly IRepository<Candidate> _candidates;

    public CandidateService(IRepository<Candidate> candidates)
    {
        _candidates = candidates;
    }

    public CandidateResponse Register(string fullName, string email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleException("Full name is required.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new BusinessRuleException("A valid email is required.");

        var emailTaken = _candidates.GetAll()
            .Any(c => c.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        if (emailTaken)
            throw new BusinessRuleException("A candidate with this email already exists.");

        var candidate = new Candidate
        {
            FullName = fullName.Trim(),
            Email = email.Trim(),
            Phone = phone?.Trim()
        };

        _candidates.Add(candidate);
        return ToResponse(candidate);
    }

    public CandidateResponse UpdateProfile(Guid candidateId, string fullName, string? phone)
    {
        var candidate = FindCandidate(candidateId);

        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessRuleException("Full name is required.");

        candidate.FullName = fullName.Trim();
        candidate.Phone = phone?.Trim();
        _candidates.Update(candidate);
        return ToResponse(candidate);
    }

    public CandidateResponse GetById(Guid candidateId) => ToResponse(FindCandidate(candidateId));

    public List<CandidateResponse> GetAll() => _candidates.GetAll().Select(ToResponse).ToList();

    private Candidate FindCandidate(Guid candidateId)
    {
        return _candidates.GetById(candidateId)
               ?? throw new NotFoundException($"Candidate {candidateId} was not found.");
    }

    private static CandidateResponse ToResponse(Candidate c) =>
        new(c.Id, c.FullName, c.Email, c.Phone, c.CreatedAt);
}
