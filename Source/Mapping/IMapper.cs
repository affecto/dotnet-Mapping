namespace Affecto.Mapping
{
    public interface IMapper<in TSource, out TDestination>
    {
        TDestination Map(TSource source);
        TDestination Map(TSource source, params (string parameterName, object parameterValue)[] parameters);
    }
}