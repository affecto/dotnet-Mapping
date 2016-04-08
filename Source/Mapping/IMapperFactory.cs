namespace Affecto.Mapping
{
    public interface IMapperFactory
    {
        IMapper<TSource, TDestination> Create<TSource, TDestination>();
    }
}