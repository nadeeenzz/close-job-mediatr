using Microsoft.EntityFrameworkCore;
using Recruitment.Domain.Entities;

namespace Recruitment.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(job =>
        {
            job.Property(j => j.Title).IsRequired().HasMaxLength(200);
            job.Property(j => j.Description).HasMaxLength(2000);
            job.Property(j => j.Status).HasConversion<string>().HasMaxLength(20); // saved as "Open"/"Closed"
        });

        modelBuilder.Entity<Candidate>(candidate =>
        {
            candidate.Property(c => c.FullName).IsRequired().HasMaxLength(200);
            candidate.Property(c => c.Email).IsRequired().HasMaxLength(200);
            candidate.Property(c => c.Phone).HasMaxLength(30);
            candidate.HasIndex(c => c.Email).IsUnique();
        });

        modelBuilder.Entity<JobApplication>(application =>
        {
            application.Property(a => a.Status).HasConversion<string>().HasMaxLength(20);

            // An application belongs to one job and one candidate.
            // Restrict = you can't delete a job/candidate that still has applications.
            application.HasOne<Job>()
                .WithMany()
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            application.HasOne<Candidate>()
                .WithMany()
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            application.HasIndex(a => new { a.JobId, a.CandidateId });
        });
    }
}
