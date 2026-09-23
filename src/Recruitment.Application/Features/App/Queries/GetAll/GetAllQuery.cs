using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.App.Queries.GetAll;

public class GetAllQuery : IRequest<IEnumerable<ApplicationResponse>>
{
}