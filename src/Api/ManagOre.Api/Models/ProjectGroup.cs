using System;

namespace ManagOre.Api.Models;

/// <summary>
/// Logical grouping for related projects (e.g. product verticals, portfolios).
/// </summary>
public class ProjectGroup
{
    /// <summary>Primary key identifier.</summary>
    public Guid Id { get; set; }
    /// <summary>Group display name.</summary>
    public string Name { get; set; } = null!;
    /// <summary>Optional description of the group.</summary>
    public string? Description { get; set; }

    // navigation
    /// <summary>Projects that belong to this group.</summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
