using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.Application.Interfaces;
using Recruitment.Application.Services;
using Recruitment.Domain.Entities;
using Recruitment.Infrastructure.Data;
using Recruitment.Infrastructure.Repositories;

namespace Recruitment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRecruitmentServices(
        this IServiceCollection services, string connectionString)
    {
        // Database (SQLite). To use SQL Server, see the README.
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        // Repositories are scoped (one per request) because DbContext is scoped.
        services.AddScoped<IRepository<Job>, EfRepository<Job>>();
        services.AddScoped<IRepository<Candidate>, EfRepository<Candidate>>();
        services.AddScoped<IJobApplicationRepository, EfJobApplicationRepository>();

        // Application services: one per entity.
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<ICandidateService, CandidateService>();

        return services;
    }
}
