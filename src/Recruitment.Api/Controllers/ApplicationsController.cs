using JobApplication.Application.Features.Applications.Commands.CancelApplication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Application.Dtos;
using Recruitment.Application.Features.App.Commands.ApplyJob;
using Recruitment.Application.Features.App.Commands.UpdateStatus;
using Recruitment.Application.Features.App.Queries.GetAll;
using Recruitment.Application.Features.App.Queries.GetById;
using Recruitment.Application.Interfaces;

namespace Recruitment.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationsController : ControllerBase
{
    //private readonly IApplicationService _applicationService;
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllQuery());

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApplicationResponse>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetByIdQuery(id));

        return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult<ApplicationResponse>> Apply(ApplyRequest request)
    {
        var application = await _mediator.Send(new ApplyJobCommand(request.JobId, request.CandidateId));

        return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<ActionResult<ApplicationResponse>> Cancel(Guid id)
    {
        var application = await _mediator.Send(new CancelApplicationCommand(id, string.Empty));

        return Ok(application);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<ApplicationResponse>> UpdateStatus(Guid id, UpdateApplicationStatusRequest request)
    {
        var application = await _mediator.Send(new UpdateStatusCommand(id, request.Status));

        return Ok(application);
    }
}
