using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public abstract class TwoWayMappingProfile<TSource, TDestination> : MappingProfile<TSource, TDestination>, ITwoWayMappingProfile<TSource, TDestination>
    {
        public new ITwoWayMapper<TSource, TDestination> CreateMapper(IMapper mapper)
        {
            return new TwoWayMapper(mapper);
        }

        protected abstract void ConfigureMapping(IMappingExpression<TDestination, TSource> map);

        protected override void Configure()
        {
            base.Configure();

            IMappingExpression<TDestination, TSource> destinationToSourceMap = CreateMap<TDestination, TSource>();
            ConfigureMapping(destinationToSourceMap);
        }

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