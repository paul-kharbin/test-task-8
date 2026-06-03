using System.Collections.Generic;

namespace TestTask.Web.Models;

public sealed class AnalyticsResponse
{
    public int CandidateCount { get; set; }
    public decimal AverageSalary { get; set; }
    public double AverageExperience { get; set; }
    public IReadOnlyList<CandidateRatingResponse> Ratings { get; set; } = [];
}
