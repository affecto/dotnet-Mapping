using System.Collections.Generic;
using Autofac;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper.Autofac
{
    public class MapperConfigurationFactory
    {
        public MapperConfiguration CreateMapperConfiguration(IComponentContext componentContext)
        {
            IEnumerable<Profile> profiles = componentContext.Resolve<IEnumerable<Profile>>();
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                foreach (Profile profile in profiles)
                {
                    config.AddProfile(profile);
                }

                AddCustomConfiguration(config);
            });

            return mapperConfiguration;
        }

        protected virtual void AddCustomConfiguration(IMapperConfiguration configuration)
        {
            // Map properties with public or internal getters
            configuration.ShouldMapProperty = p => (p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly));
        }
    }
}