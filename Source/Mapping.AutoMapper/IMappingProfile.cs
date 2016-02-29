using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public interface IMappingProfile<in TSource, out TDestination>
    {
        IMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}