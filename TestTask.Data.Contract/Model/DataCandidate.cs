using System;
using TestTask.Data.Contract.Bases;

namespace TestTask.Data.Contract.Model;

public sealed class DataCandidate : DataModelBase
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public decimal DesiredSalary { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
