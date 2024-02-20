namespace Persistence;

public interface IMapToDatabaseRecords<in TFrom, TTo>
{
    Task<TTo> Map(TFrom from, CancellationToken cancellationToken = default);
}