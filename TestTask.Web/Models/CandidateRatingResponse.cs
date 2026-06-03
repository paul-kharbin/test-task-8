namespace TestTask.Web.Models;

public sealed class CandidateRatingResponse
{
    public CandidateResponse Candidate { get; set; } = new();
    public int Score { get; set; }
}
