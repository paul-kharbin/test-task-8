namespace TestTask.Web.Models;

public sealed class SessionResponse
{
    public bool IsAuthenticated { get; set; }
    public string? Login { get; set; }
}
