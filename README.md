# Recruitment API

ASP.NET Core 8 Web API. Application-layer services are grouped **by entity, not by action**:

| Service            | Methods                                   |
|--------------------|-------------------------------------------|
| `ApplicationService` | `Apply()`, `Cancel()`, `UpdateStatus()` |
| `JobService`         | `Close()`, `Reopen()`                   |
| `CandidateService`   | `Register()`, `UpdateProfile()`         |

(Each also has a few basic `Create` / `GetById` / `GetAll` methods so the API is usable.)

## Structure
```
src/
  Recruitment.Domain          Entities + enums (no dependencies)
  Recruitment.Application     Services, interfaces, DTOs, exceptions   <- the "one service per entity" layer
  Recruitment.Infrastructure  EF Core DbContext, repositories, DI registration
  Recruitment.Api             Controllers, exception middleware, Program.cs
```
Dependencies point inward: Api -> Infrastructure -> Application -> Domain.

## Run (first time)
```
dotnet tool install --global dotnet-ef --version 8.*
dotnet restore

dotnet ef migrations add InitialCreate --project src/Recruitment.Infrastructure --startup-project src/Recruitment.Api --output-dir Data/Migrations

dotnet run --project src/Recruitment.Api
```
Swagger opens at http://localhost:5080/swagger. The SQLite file `recruitment.db` is created next to the API project on startup.

## Changing the model later
```
dotnet ef migrations add <Name> --project src/Recruitment.Infrastructure --startup-project src/Recruitment.Api --output-dir Data/Migrations
```
Migrations are applied automatically when the app starts.

## Switch to SQL Server
1. In `Recruitment.Infrastructure.csproj` replace `Microsoft.EntityFrameworkCore.Sqlite` with `Microsoft.EntityFrameworkCore.SqlServer` (8.0.*).
2. In `DependencyInjection.cs` change `options.UseSqlite(...)` to `options.UseSqlServer(...)`.
3. In `appsettings.json` set e.g. `Server=(localdb)\\MSSQLLocalDB;Database=Recruitment;Trusted_Connection=True;`
4. Delete the `Data/Migrations` folder and create the migration again (step above).

## Endpoints
| Method | Route | Service call |
|---|---|---|
| POST | /api/applications | ApplicationService.Apply |
| POST | /api/applications/{id}/cancel | ApplicationService.Cancel |
| PATCH | /api/applications/{id}/status | ApplicationService.UpdateStatus |
| POST | /api/jobs/{id}/close | JobService.Close |
| POST | /api/jobs/{id}/reopen | JobService.Reopen |
| POST | /api/candidates/register | CandidateService.Register |
| PUT | /api/candidates/{id}/profile | CandidateService.UpdateProfile |

## Business rules
- Can't apply to a closed job, or apply twice to the same job
- Can't cancel an Accepted / Rejected / already Cancelled application
- Can't change the status of a cancelled application
- Can't close a closed job or reopen an open job
- Candidate email must be unique

## Notes
- Services depend on `IRepository<T>` only, so the database change did not touch the Application layer.
- The entity is called `JobApplication` (not `Application`) to avoid a name clash with the `Recruitment.Application` namespace.
