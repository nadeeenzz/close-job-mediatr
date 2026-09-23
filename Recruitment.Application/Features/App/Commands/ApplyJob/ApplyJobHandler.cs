using MediatR;
using Recruitment.Application.Exceptions;
using Recruitment.Application.Interfaces;
using Recruitment.Domain.Entities;
using Recruitment.Domain.Enums;
using Recruitment.Application.Dtos;

namespace Recruitment.Application.Features.App.Commands.ApplyJob
{
    public class ApplyJobHandler : IRequestHandler<ApplyJobCommand , ApplicationResponse>
    {
        private readonly IJobService _jobs;
        private readonly ICandidateService _candidates;
        private readonly IJobApplicationRepository _applications;

        public ApplyJobHandler(
            IJobService jobs,
            ICandidateService candidates,
            IJobApplicationRepository applications)
        {
            _jobs = jobs;
            _candidates = candidates;
            _applications = applications;
        }

        public Task<ApplicationResponse> Handle(
            ApplyJobCommand request,
            CancellationToken cancellationToken)
        {
            var job = _jobs.GetById(request.JobId)
               ?? throw new NotFoundException("Job", request.JobId);

            if (_candidates.GetById(request.CandidateId) is null)
                throw new NotFoundException("Candidate", request.CandidateId);

            if (job.Status == JobStatus.Closed)
                throw new BusinessRuleException(
                    "You cannot apply to a closed job.");

            if (_applications.HasActiveApplication(
                    request.JobId,
                    request.CandidateId))
                throw new BusinessRuleException(
                    "Candidate already applied to this job.");

            var application = new Recruitment.Domain.Entities.JobApplication
            {
                JobId = request.JobId,
                CandidateId = request.CandidateId
            };

            _applications.Add(application);

            var response = new ApplicationResponse(
                application.Id,
                application.JobId,
                application.CandidateId,
                application.Status,
                application.AppliedAt
            );

            return Task.FromResult(response);
        }
    }
}