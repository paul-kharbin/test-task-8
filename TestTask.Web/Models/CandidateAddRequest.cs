using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Web.Models;

public sealed class CandidateAddRequest
{
    [Required(ErrorMessage = "Укажите ФИО")]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите дату рождения")]
    public DateTime BirthDate { get; set; }

    [Range(1, 10000000, ErrorMessage = "Укажите положительное значение зарплаты")]
    public decimal DesiredSalary { get; set; }

    [Required(ErrorMessage = "Укажите email")]
    [EmailAddress(ErrorMessage = "Введите корректный email")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите желаемую должность")]
    [StringLength(100)]
    public string Position { get; set; } = string.Empty;

    [Range(0, 60, ErrorMessage = "Стаж должен быть от 0 до 60 лет")]
    public int ExperienceYears { get; set; }
}
