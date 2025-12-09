using System;

namespace ManagOre.Api.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ProjectGroupId { get; set; }

    // navigation
    public ProjectGroup? ProjectGroup { get; set; }
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
