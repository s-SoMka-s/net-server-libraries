namespace Db.Types;
public interface IBaseDataType
{
    long Id { get; set; }

    DateTimeOffset CreatedAt { get; set; }

    DateTimeOffset UpdatedAt { get; set; }

    bool IsDeleted { get; set; }
}
