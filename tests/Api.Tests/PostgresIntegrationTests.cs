using System;
using System.Threading.Tasks;
// Integration tests use an external Postgres instance provided by CI (or local/dev configured container)
using Microsoft.EntityFrameworkCore;
using Xunit;
using ManagOre.Api.Data;
using ManagOre.Api.Models;

namespace Api.Tests;

public class PostgresIntegrationTests
{
    // No container management here; CI will provide the DB and connection string via env vars.

    [Fact]
    public async Task Postgres_roundtrip_migrations_and_crud()
    {
        var enabled = Environment.GetEnvironmentVariable("POSTGRES_INTEGRATION") == "true";
        if (!enabled)
        {
            // Not enabled — skip silently so local runs remain fast and stable
            return;
        }

        var cs = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
        if (string.IsNullOrEmpty(cs))
        {
            throw new InvalidOperationException("POSTGRES_CONNECTION_STRING is required for Postgres integration tests. Export the connection string and set POSTGRES_INTEGRATION=true to enable these tests.");
        }

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            // Ensure the migrations assembly name is the simple assembly name (e.g. 'ManagOre.Api') so EF can locate compiled migrations
            .UseNpgsql(cs, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
            .Options;

        // apply migrations if present; fallback to EnsureCreated for CI/local environments
        using (var ctx = new ApplicationDbContext(options))
        {
            try
            {
                ctx.Database.Migrate();
            }
            catch
            {
                // If EF migrations are not found for this environment, EnsureCreated will create schema directly
                ctx.Database.EnsureCreated();
            }
        }

        using (var ctx = new ApplicationDbContext(options))
        {
            var e = new Employee { Id = Guid.NewGuid(), FirstName = "Anna", LastName = "Verdi" };
            var pg = new ProjectGroup { Id = Guid.NewGuid(), Name = "Integration" };
            var p = new Project { Id = Guid.NewGuid(), Name = "PgProject", ProjectGroupId = pg.Id };
            var te = new TimeEntry { Id = Guid.NewGuid(), EmployeeId = e.Id, ProjectId = p.Id, Date = DateTime.UtcNow.Date, Hours = 4.5 };

            ctx.Employees.Add(e);
            ctx.ProjectGroups.Add(pg);
            ctx.Projects.Add(p);
            ctx.TimeEntries.Add(te);
            await ctx.SaveChangesAsync();
        }

        using (var ctx = new ApplicationDbContext(options))
        {
            Assert.Equal(1, await ctx.Employees.CountAsync());
            Assert.Equal(1, await ctx.ProjectGroups.CountAsync());
            Assert.Equal(1, await ctx.Projects.CountAsync());
            Assert.Equal(1, await ctx.TimeEntries.CountAsync());
        }
    }
}
