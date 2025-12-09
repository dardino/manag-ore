using System;

namespace ManagOre.Api.Models;

/// <summary>
/// Represents an employee who can report timesheet entries.
/// </summary>
public class Employee
{
    /// <summary>Primary key identifier.</summary>
    public Guid Id { get; set; }
    /// <summary>Employee first name.</summary>
    public string FirstName { get; set; } = null!;
    /// <summary>Employee last name.</summary>
    public string LastName { get; set; } = null!;
    /// <summary>Optional email address.</summary>
    public string? Email { get; set; }

    // navigation
    /// <summary>Navigation property to timesheet entries.</summary>
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
