using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.Job.Commands.Reopen;

public class ReopenCommand : IRequest<JobResponse>
{
    public Guid JobId { get; set; }

    public ReopenCommand(Guid jobId)
    {
        JobId = jobId;
    }
}