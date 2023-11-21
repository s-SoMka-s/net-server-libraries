using System.ComponentModel.DataAnnotations;

namespace Net.Server.Libraries.Db.Types;

/// <summary>
/// Базовый тип для всех моделей EF Core. Содержит такие поля как:
/// Id - ключевое поле
/// CreatedAt - дата создания
/// UpdatedAt - дата последнего обновления
/// IsDeleted - Было ли удалено поле
/// </summary>
public class BaseDataType : IBaseDataType
{
    protected BaseDataType()
    {
        Id = default;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
        IsDeleted = false;
    }

    [Key]
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}
