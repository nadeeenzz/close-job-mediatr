using Microsoft.AspNetCore.Mvc;
using Recruitment.Application.Dtos;
using Recruitment.Application.Interfaces;

namespace Recruitment.Api.Controllers;

[ApiController]
[Route("api/candidates")]
public class CandidatesController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet]
    public ActionResult<List<CandidateResponse>> GetAll() => _candidateService.GetAll();

    [HttpGet("{id:guid}")]
    public ActionResult<CandidateResponse> GetById(Guid id) => _candidateService.GetById(id);

    [HttpPost("register")]
    public ActionResult<CandidateResponse> Register(RegisterCandidateRequest request)
    {
        var candidate = _candidateService.Register(request.FullName, request.Email, request.Phone);
        return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
    }

    [HttpPut("{id:guid}/profile")]
    public ActionResult<CandidateResponse> UpdateProfile(Guid id, UpdateCandidateProfileRequest request)
    {
        return _candidateService.UpdateProfile(id, request.FullName, request.Phone);
    }
}
