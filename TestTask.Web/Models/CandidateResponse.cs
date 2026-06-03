using System;

namespace TestTask.Web.Models;

public sealed class CandidateResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public decimal DesiredSalary { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
