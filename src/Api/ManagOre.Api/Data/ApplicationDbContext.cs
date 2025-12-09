using Microsoft.EntityFrameworkCore;
using ManagOre.Api.Models;

namespace ManagOre.Api.Data;

/// <summary>
/// EF Core application DB context. Contains core sets for the main entities used by the POC API.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Create a new <see cref="ApplicationDbContext"/> with the given options.
    /// </summary>
    /// <param name="options">DB context options</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>Timesheet entries.</summary>
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
    /// <summary>Employees table.</summary>
    public DbSet<Employee> Employees { get; set; } = null!;
    /// <summary>Projects table.</summary>
    public DbSet<Project> Projects { get; set; } = null!;
    /// <summary>Project groups table.</summary>
    public DbSet<ProjectGroup> ProjectGroups { get; set; } = null!;

    /// <summary>
    /// Configure EF model mappings and constraints.
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(b =>
        {
            b.HasKey(e => e.Id);
            b.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            b.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            b.Property(e => e.Email).HasMaxLength(200);
        });

        modelBuilder.Entity<ProjectGroup>(b =>
        {
            b.HasKey(pg => pg.Id);
            b.Property(pg => pg.Name).IsRequired().HasMaxLength(200);
            b.Property(pg => pg.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Project>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Name).IsRequired().HasMaxLength(200);
            b.HasOne(p => p.ProjectGroup).WithMany(pg => pg.Projects).HasForeignKey(p => p.ProjectGroupId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TimeEntry>(b =>
        {
            b.HasKey(t => t.Id);
            b.Property(t => t.Hours).IsRequired();
            b.HasOne(t => t.Employee).WithMany(e => e.TimeEntries).HasForeignKey(t => t.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            b.HasOne(t => t.Project).WithMany(p => p.TimeEntries).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
