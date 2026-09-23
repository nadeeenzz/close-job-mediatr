using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Recruitment.Infrastructure.Data;

public static class DatabaseInitializer
{
    // Creates the database (if missing) and applies any pending migrations.
    public static IHost ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        return host;
    }
}
