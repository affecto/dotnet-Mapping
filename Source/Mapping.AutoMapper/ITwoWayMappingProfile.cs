using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public interface ITwoWayMappingProfile<TSource, TDestination>
    {
        ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}