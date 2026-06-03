using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Models;

public sealed class UserLoginRrequest
{
    [Required(ErrorMessage = "Укажите логин")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; } = string.Empty;
}
