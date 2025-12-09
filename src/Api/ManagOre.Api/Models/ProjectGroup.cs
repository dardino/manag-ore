using System;

namespace ManagOre.Api.Models;

public class ProjectGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // navigation
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
