using System;

namespace ManagOre.Api.Models;

/// <summary>
/// A single time report for a user on a given date and project.
/// </summary>
public class TimeEntry
{
  /// <summary>Primary key identifier.</summary>
  public Guid Id { get; set; }
  /// <summary>Reference to the employee who logged time.</summary>
  public Guid EmployeeId { get; set; }
  /// <summary>Reference to the project.</summary>
  public Guid ProjectId { get; set; }
  /// <summary>Date for this time entry.</summary>
  public DateTime Date { get; set; }
  /// <summary>Number of hours recorded.</summary>
  public double Hours { get; set; }
  /// <summary>Optional free-form description.</summary>
  public string? Description { get; set; }

  // navigation
  /// <summary>Employee referenced by this time entry.</summary>
  public Employee? Employee { get; set; }
  /// <summary>Project referenced by this time entry.</summary>
  public Project? Project { get; set; }
}
