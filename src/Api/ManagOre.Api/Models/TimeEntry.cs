using System;

namespace ManagOre.Api.Models;

public class TimeEntry
{
  public Guid Id { get; set; }
  public Guid EmployeeId { get; set; }
  public Guid ProjectId { get; set; }
  public DateTime Date { get; set; }
  public double Hours { get; set; }
  public string? Description { get; set; }
}
