using System;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    [Obsolete("Two-way mapping is deprecated and it is going to be removed in a future major version.", false)]
    public abstract class TwoWayMappingProfile<TSource, TDestination> : MappingProfile<TSource, TDestination>, ITwoWayMappingProfile<TSource, TDestination>
    {
        protected TwoWayMappingProfile()
        {
            IMappingExpression<TDestination, TSource> destinationToSourceMap = CreateMap<TDestination, TSource>();
            ConfigureMapping(destinationToSourceMap);
        }

        public new ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper)
        {
            return new TwoWayMapper(mapper);
        }

        protected abstract void ConfigureMapping(IMappingExpression<TDestination, TSource> map);

        [Obsolete("Two-way mapping is deprecated and it is going to be removed in a future major version.", false)]
        public class TwoWayMapper : Mapper, ITwoWayMapper<TSource, TDestination>
        {
            public TwoWayMapper(IMapper mapper)
                : base(mapper)
            {
            }

            public virtual TSource Map(TDestination source)
            {
                return mapper.Map<TDestination, TSource>(source);
            }
        }
    }
}