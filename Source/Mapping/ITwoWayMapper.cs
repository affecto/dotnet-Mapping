namespace Affecto.Mapping
{
    public interface ITwoWayMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);
        TSource Map(TDestination source, params (string parameterName, object parameterValue)[] parameters);
    }
}