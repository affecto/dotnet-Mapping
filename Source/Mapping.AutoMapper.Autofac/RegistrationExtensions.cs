using System;
using Autofac;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper.Autofac
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Configures AutoMapper with all mapping profiles currently registered to Autofac container.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="mapperConfigurationFactory">Custom AutoMapper configuration factory.</param>
        public static void ConfigureAutoMapper(this ContainerBuilder builder, AutofacMapperConfigurationFactory mapperConfigurationFactory)
        {
            builder.Register(mapperConfigurationFactory.CreateMapperConfiguration)
                .SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>();

            builder.RegisterType<AutofacMapperFactory>().As<IMapperFactory>();
        }

        /// <summary>
        /// Configures AutoMapper with all mapping profiles currently registered to Autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureAutoMapper(this ContainerBuilder builder)
        {
            ConfigureAutoMapper(builder, new AutofacMapperConfigurationFactory());
        }

        /// <summary>
        /// Registers a mapping profile and corresponding mapper to Autofac container.
        /// </summary>
        /// <typeparam name="TProfile">Mapping profile type.</typeparam>
        /// <typeparam name="TSource">Mapping source type.</typeparam>
        /// <typeparam name="TDestination">Mapping destination type.</typeparam>
        public static void RegisterMappingProfile<TProfile, TSource, TDestination>(this ContainerBuilder builder)
            where TProfile : Profile, IMappingProfile<TSource, TDestination>
        {
            builder.RegisterType<TProfile>()
                .SingleInstance()
                .AsSelf()
                .As<Profile>();

            builder.Register(ResolveMapper<TProfile, TSource, TDestination>);
        }

        /// <summary>
        /// Registers a two-way mapping profile and corresponding mapper to Autofac container.
        /// </summary>
        /// <typeparam name="TProfile">Mapping profile type.</typeparam>
        /// <typeparam name="TSource">Mapping source type.</typeparam>
        /// <typeparam name="TDestination">Mapping destination type.</typeparam>
        [Obsolete("Two-way mapping is deprecated and it is going to be removed in a future major version.", false)]
        public static void RegisterTwoWayMappingProfile<TProfile, TSource, TDestination>(this ContainerBuilder builder)
            where TProfile : Profile, ITwoWayMappingProfile<TSource, TDestination>
        {
            builder.RegisterType<TProfile>()
                .SingleInstance()
                .AsSelf()
                .As<Profile>();

            builder.Register(ResolveTwoWayMapper<TProfile, TSource, TDestination>);
        }

        private static IMapper<TSource, TDestination> ResolveMapper<TProfile, TSource, TDestination>(IComponentContext context)
            where TProfile : IMappingProfile<TSource, TDestination>
        {
            IMapper autoMapper = context.Resolve<IMapper>();
            TProfile profile = context.Resolve<TProfile>();

            return profile.CreateMapper(autoMapper);
        }

        private static ITwoWayMapper<TSource, TDestination> ResolveTwoWayMapper<TProfile, TSource, TDestination>(IComponentContext context)
            where TProfile : ITwoWayMappingProfile<TSource, TDestination>
        {
            IMapper autoMapper = context.Resolve<IMapper>();
            TProfile profile = context.Resolve<TProfile>();

            return profile.CreateMapper(autoMapper);
        }
    }
}