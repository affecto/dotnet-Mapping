using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    /// <summary>
    /// Factory class for creating AutoMapper configuration using mapping profiles.
    /// </summary>
    public class MapperConfigurationFactory
    {
        /// <summary>
        /// Creates AutoMapper configuration using mapping profiles.
        /// </summary>
        /// <param name="profiles">A set of mapping profiles.</param>
        public MapperConfiguration CreateMapperConfiguration(IEnumerable<Profile> profiles)
        {
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
        /// Creates AutoMapper configuration using mapping profiles.
        /// </summary>
        /// <param name="profiles">A set of mapping profiles.</param>
        public MapperConfiguration CreateMapperConfiguration(params Profile[] profiles)
        {
            return CreateMapperConfiguration((IEnumerable<Profile>) profiles);
        }

        /// <summary>
        /// Creates AutoMapper configuration using mapping profiles defined in an assembly.
        /// </summary>
        /// <param name="assembly">Assembly to where search for mapping profiles.</param>
        public MapperConfiguration CreateMapperConfiguration(Assembly assembly)
        {
            Profile[] profiles = assembly.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(Profile)))
                .Select(t => Activator.CreateInstance(t.AsType()))
                .Cast<Profile>()
                .ToArray();

            return CreateMapperConfiguration(profiles);
        }

        /// <summary>
        /// Set custom AutoMapper configuration settings.
        /// </summary>
        /// <param name="configuration">AutoMapper configuration.</param>
        protected virtual void AddCustomConfiguration(IMapperConfigurationExpression configuration)
        {
            // Map properties with public or internal getters
            configuration.ShouldMapProperty = p => (p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly));
        }
    }
}