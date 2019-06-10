using System;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    [Obsolete]
    public interface ITwoWayMappingProfile<TSource, TDestination>
    {
        ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}