using Microsoft.EntityFrameworkCore;
using ManagOre.Api.Models;

namespace ManagOre.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
    // Add Employees, Projects, ProjectGroups later
}
