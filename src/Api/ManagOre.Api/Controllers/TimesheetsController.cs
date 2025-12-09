using Microsoft.AspNetCore.Mvc;
using ManagOre.Api.Data;
using ManagOre.Api.Models;

/// <summary>
/// Controller for simple timesheet CRUD operations used by the POC API.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TimesheetsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Create a controller instance.
    /// </summary>
    /// <param name="db">Application DB context</param>
    public TimesheetsController(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Returns up to 50 timesheet entries.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var list = _db.TimeEntries.Take(50).ToList();
        return Ok(list);
    }

    /// <summary>
    /// Returns a single timesheet entry by id.
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var item = _db.TimeEntries.Find(id);
        return item == null ? NotFound() : Ok(item);
    }

    /// <summary>
    /// Create a new timesheet entry.
    /// </summary>
    [HttpPost]
    public IActionResult Create(TimeEntry item)
    {
        if (item.Id == Guid.Empty) item.Id = Guid.NewGuid();
        _db.TimeEntries.Add(item);
        _db.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    /// <summary>
    /// Update an existing timesheet entry.
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, TimeEntry input)
    {
        var found = _db.TimeEntries.Find(id);
        if (found == null) return NotFound();
        found.Hours = input.Hours;
        found.Description = input.Description;
        _db.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Delete a timesheet entry.
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var found = _db.TimeEntries.Find(id);
        if (found == null) return NotFound();
        _db.TimeEntries.Remove(found);
        _db.SaveChanges();
        return NoContent();
    }
}
