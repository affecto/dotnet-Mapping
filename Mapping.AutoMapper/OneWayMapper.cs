using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public abstract class OneWayMapper<TSource, TDestination> : AutoMapConfigurator<OneWayMapper<TSource, TDestination>>, IMapper<TSource, TDestination>
    {
        protected OneWayMapper()
        {
        }

        protected OneWayMapper(bool initializeMaps)
            : base(initializeMaps)
        {
        }
      
        /// <summary>
        /// Abstract method for defining auto maps. Use static Mapper class to define maps.
        /// </summary>
        protected abstract override void ConfigureMaps();

        public virtual TDestination Map(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}