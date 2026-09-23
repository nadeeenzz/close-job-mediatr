using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.Job.Commands.Create;

public class CreateCommand : IRequest<JobResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }

    public CreateCommand(string title, string description)
    {
        Title = title;
        Description = description;
    }
}