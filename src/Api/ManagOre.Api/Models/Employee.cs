using System;

namespace ManagOre.Api.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }

    // navigation
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
