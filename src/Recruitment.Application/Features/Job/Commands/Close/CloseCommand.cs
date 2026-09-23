using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.Job.Commands.Close;

public class CloseCommand : IRequest<JobResponse>
{
    public Guid JobId { get; set; }

    public CloseCommand(Guid jobId)
    {
        JobId = jobId;
    }
}