namespace Domain.Abstractions;

public interface IMapToTheDomain<in TFrom, TTo>
{
    Task<TTo> Map(TFrom from, CancellationToken cancellationToken = default);
}