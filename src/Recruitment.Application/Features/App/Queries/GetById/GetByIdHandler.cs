using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;

namespace Recruitment.Application.Features.App.Queries.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, ApplicationResponse>
{
    private readonly IApplicationService _applicationService;

    public GetByIdHandler(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task<ApplicationResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var application = _applicationService.GetById(request.Id)
            ?? throw new NotFoundException("Application", request.Id);

        return Task.FromResult(application);
    }
}