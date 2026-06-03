using System;
using TestTask.Infrastructure.Contact.Bases;

namespace TestTask.Infrastructure.Contact.Model;

public sealed class Candidate : ModelBase
{
    public string FullName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public decimal DesiredSalary { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
