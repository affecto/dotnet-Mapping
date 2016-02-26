using Autofac;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper.Autofac
{
    public static class RegistrationExtensions
    {
        public static void ConfigureAutoMapper(this ContainerBuilder builder, MapperConfigurationFactory mapperConfigurationFactory)
        {
            builder.Register(mapperConfigurationFactory.CreateMapperConfiguration)
                .SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>();
        }

        public static void ConfigureAutoMapper(this ContainerBuilder builder)
        {
            ConfigureAutoMapper(builder, new MapperConfigurationFactory());
        }

        public static void RegisterMappingProfile<TProfile, TSource, TDestination>(this ContainerBuilder builder)
            where TProfile : Profile, IMappingProfile<TSource, TDestination>
        {
            builder.RegisterType<TProfile>()
                .SingleInstance()
                .AsSelf()
                .As<Profile>();

            builder.Register(ResolveMapper<TProfile, TSource, TDestination>);
        }

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