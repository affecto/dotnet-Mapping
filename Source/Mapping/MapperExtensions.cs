using System.Collections.Generic;
using System.Linq;

namespace Affecto.Mapping
{
    public static class MapperExtensions
    {
        public static ICollection<TDestination> Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> sourceCollection)
        {
            return sourceCollection == null ? new List<TDestination>(0) : sourceCollection.Select(mapper.Map).ToList();
        }

        public static ICollection<TSource> Map<TSource, TDestination>(this ITwoWayMapper<TSource, TDestination> mapper, IEnumerable<TDestination> sourceCollection)
        {
            return sourceCollection == null ? new List<TSource>(0) : sourceCollection.Select(mapper.Map).ToList();
        }
    }
}