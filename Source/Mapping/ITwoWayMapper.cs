using System;

namespace Affecto.Mapping
{
    [Obsolete("Two-way mapping is deprecated and it is going to be removed in a future major version.", false)]
    public interface ITwoWayMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        TSource Map(TDestination source);
    }
}