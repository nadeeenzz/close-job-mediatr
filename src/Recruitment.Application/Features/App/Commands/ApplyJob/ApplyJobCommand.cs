using MediatR;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.App.Commands.ApplyJob;

public record ApplyJobCommand(Guid JobId, Guid CandidateId) : IRequest<ApplicationResponse>;