using TestTask.Data.Contract.Bases;

namespace TestTask.Data.Contract.Model;

public sealed class DataUser : DataModelBase
{
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
