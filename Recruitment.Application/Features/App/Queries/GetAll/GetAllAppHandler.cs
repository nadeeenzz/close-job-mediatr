using MediatR;
using Recruitment.Application.Dtos;
using Recruitment.Application.Interfaces;

namespace Recruitment.Application.Features.App.Queries.GetAll;

public class GetAllAppHandler : IRequestHandler<GetAllQuery, IEnumerable<ApplicationResponse>>
{
    private readonly IApplicationService _applicationService;

    public GetAllAppHandler(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task<IEnumerable<ApplicationResponse>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var applications = _applicationService.GetAll();

        return Task.FromResult(applications);
    }
}