using System;

namespace ManagOre.Api.Models;

/// <summary>
/// Represents a single project which belongs optionally to a ProjectGroup.
/// </summary>
public class Project
{
    /// <summary>Primary key identifier.</summary>
    public Guid Id { get; set; }
    /// <summary>Project name.</summary>
    public string Name { get; set; } = null!;
    /// <summary>Optional short description.</summary>
    public string? Description { get; set; }
    /// <summary>Optional link to a parent ProjectGroup.</summary>
    public Guid? ProjectGroupId { get; set; }

    // navigation
    /// <summary>Optional parent project group.</summary>
    public ProjectGroup? ProjectGroup { get; set; }
    /// <summary>Timesheet entries for this project.</summary>
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
