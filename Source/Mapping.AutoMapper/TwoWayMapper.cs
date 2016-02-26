using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public abstract class TwoWayMapper<TSource, TDestination> : OneWayMapper<TSource, TDestination>, ITwoWayMapper<TSource, TDestination>
    {
        protected TwoWayMapper()
        {
        }

        protected TwoWayMapper(bool initializeMaps)
            : base(initializeMaps)
        {
        }

        /// <summary>
        /// Abstract method for defining auto maps. Use static Mapper class to define maps.
        /// </summary>
        protected abstract override void ConfigureMaps();

        public virtual TSource Map(TDestination source)
        {
            return Mapper.Map<TDestination, TSource>(source);
        }
    }
}