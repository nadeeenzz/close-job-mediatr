using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.Job.Queries.GetAllJobs;

public class GetAllJobsQuery : IRequest<IEnumerable<JobResponse>>
{
}