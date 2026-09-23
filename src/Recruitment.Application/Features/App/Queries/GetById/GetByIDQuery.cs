using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.App.Queries.GetById;

public record GetByIdQuery(Guid Id) : IRequest<ApplicationResponse>;