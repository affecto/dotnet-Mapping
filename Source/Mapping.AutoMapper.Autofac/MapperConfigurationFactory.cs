using System.Collections.Generic;
using Autofac;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper.Autofac
{
    /// <summary>
    /// Factory class for creating AutoMapper configuration using profiles registered to Autofac container.
    /// </summary>
    public class MapperConfigurationFactory
    {
        /// <summary>
        /// Creates AutoMapper configuration using profiles registered to Autofac container.
        /// </summary>
        /// <param name="componentContext">Autofac component context.</param>
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

        /// <summary>
        /// Set custom AutoMapper configuration settings.
        /// </summary>
        /// <param name="configuration">AutoMapper configuration.</param>
        protected virtual void AddCustomConfiguration(IMapperConfiguration configuration)
        {
            // Map properties with public or internal getters
            configuration.ShouldMapProperty = p => (p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly));
        }
    }
}