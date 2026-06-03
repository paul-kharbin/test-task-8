using TestTask.Infrastructure.Contact.Bases;

namespace TestTask.Infrastructure.Contact.Model;

public sealed class User : ModelBase
{
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
