using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;

namespace Recruitment.Application.Features.Job.Commands.Close;

public class CloseHandler    : IRequestHandler<CloseCommand, JobResponse>
{
    private readonly IJobService _jobService;

    public CloseHandler(IJobService jobService)
    {
        _jobService = jobService;
    }

    public Task<JobResponse> Handle(CloseCommand request, CancellationToken cancellationToken)
    {
        
        var job = _jobService.GetById(request.JobId);
        if (job == null)
        {
            throw new NotFoundException("Job", request.JobId);
        }

        var response = _jobService.Close(request.JobId);

        return Task.FromResult(response);
    }
}