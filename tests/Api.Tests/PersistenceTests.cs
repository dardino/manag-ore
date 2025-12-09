using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ManagOre.Api.Data;
using ManagOre.Api.Models;

namespace Api.Tests;

public class PersistenceTests : IDisposable
{
    private readonly SqliteConnection _conn;

    public PersistenceTests()
    {
        _conn = new SqliteConnection("DataSource=:memory:");
        _conn.Open();
    }

    [Fact]
    public void CanApplyMigrationsAndSaveEntities()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_conn)
            .Options;

        // apply migrations
        using (var ctx = new ApplicationDbContext(options))
        {
            ctx.Database.EnsureCreated();
        }

        // do a roundtrip
        using (var ctx = new ApplicationDbContext(options))
        {
            var employee = new Employee { Id = Guid.NewGuid(), FirstName = "Marco", LastName = "Rossi", Email = "marco@acme.com" };
            var group = new ProjectGroup { Id = Guid.NewGuid(), Name = "Core Projects" };
            var project = new Project { Id = Guid.NewGuid(), Name = "Alpha", ProjectGroupId = group.Id };
            var entry = new TimeEntry { Id = Guid.NewGuid(), EmployeeId = employee.Id, ProjectId = project.Id, Date = DateTime.UtcNow.Date, Hours = 8.0, Description = "Initial work" };

            ctx.Employees.Add(employee);
            ctx.ProjectGroups.Add(group);
            ctx.Projects.Add(project);
            ctx.TimeEntries.Add(entry);
            ctx.SaveChanges();
        }

        using (var ctx = new ApplicationDbContext(options))
        {
            Assert.Equal(1, ctx.Employees.CountAsync().Result);
            Assert.Equal(1, ctx.ProjectGroups.CountAsync().Result);
            Assert.Equal(1, ctx.Projects.CountAsync().Result);
            Assert.Equal(1, ctx.TimeEntries.CountAsync().Result);
        }
    }

    public void Dispose()
    {
        _conn?.Dispose();
    }
}
