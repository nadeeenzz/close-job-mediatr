using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;

namespace Recruitment.Application.Features.Job.Commands.Reopen;

public class ReopenHandler : IRequestHandler<ReopenCommand, JobResponse>
{
    private readonly IJobService _jobService;

    public ReopenHandler(IJobService jobService)
    {
        _jobService = jobService;
    }

    public Task<JobResponse> Handle(ReopenCommand request, CancellationToken cancellationToken)
    {
        var job = _jobService.GetById(request.JobId);
        if (job == null)
        {
            throw new NotFoundException("Job", request.JobId);
        }

        var response = _jobService.Reopen(request.JobId);

        return Task.FromResult(response);
    }
}