using System;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    [Obsolete("Two-way mapping is deprecated and it is going to be removed in a future major version.", false)]
    public interface ITwoWayMappingProfile<TSource, TDestination>
    {
        ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper);
    }
}