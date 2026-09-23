using System.Text.Json.Serialization;
using Recruitment.Api.Middleware;
using Recruitment.Infrastructure;
using Recruitment.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    // Show enums as text ("Open") instead of numbers (0)
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Data Source=recruitment.db";
builder.Services.AddRecruitmentServices(connectionString);

var app = builder.Build();

// Create the database / apply migrations when the app starts
app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

// Needed so integration tests can reference Program later
public partial class Program { }
