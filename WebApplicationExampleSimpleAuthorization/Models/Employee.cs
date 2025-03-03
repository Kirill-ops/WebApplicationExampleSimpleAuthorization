using System.ComponentModel.DataAnnotations;

namespace WebApplicationExampleSimpleAuthorization.Models;

/// <summary>
/// Модель данных для представления сотрудников
/// </summary>
public class Employee
{
    // Я убрал из него все атрибуты, потому что этот класс не должен напрямую в вебе использоваться в качестве тела/ответа запроса

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Фамилия сотрудника
    /// </summary>
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Имя сотрудника
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Отчество сотрудника
    /// </summary>
    public string Patronymic { get; set; } = string.Empty;

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Зашифрованный пароль
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Занятость
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public double Longitude { get; set; }
}
