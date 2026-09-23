using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recruitment.Application.Dtos;
using Recruitment.Application.Features.App.Queries.GetAll;
using Recruitment.Application.Features.App.Queries.GetById;
using Recruitment.Application.Features.Job.Commands.Close;
using Recruitment.Application.Features.Job.Commands.Create;
using Recruitment.Application.Features.Job.Commands.Reopen;
using Recruitment.Application.Interfaces;

namespace Recruitment.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;
    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllQuery());

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobResponse>> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetByIdQuery(id));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<JobResponse>> Create(CreateJobRequest request)
    {
        var job = await _mediator.Send(new CreateCommand(request.Title, request.Description));

        return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
    }
    [HttpPost("{id:guid}/close")]
    public async Task<ActionResult<JobResponse>> Close(Guid id)
    {
        var response = await _mediator.Send(new CloseCommand(id));

        return Ok(response);
    }


    [HttpPost("{id:guid}/reopen")]
    public async Task<ActionResult<JobResponse>> Reopen(Guid id)
    {
        var response = await _mediator.Send(new ReopenCommand(id));

        return Ok(response);
    }
}
