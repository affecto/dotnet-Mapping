using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    /// <summary>
    /// Factory class for creating mapper instances. Use as a singleton.
    /// </summary>
    public class MapperFactory : IMapperFactory
    {
        private readonly IReadOnlyCollection<Profile> profiles;
        private readonly IMapper mapper;

        /// <summary>
        /// Construct using a custom, derived MapperConfigurationFactory instance and a collection of specified mapping profiles.
        /// </summary>
        /// <param name="mapperConfigurationFactory">Custom configuration factory for providing global configuration for all mappers.</param>
        /// <param name="profiles">Mapping profiles to configure specific type mappers.</param>
        public MapperFactory(MapperConfigurationFactory mapperConfigurationFactory, IEnumerable<Profile> profiles)
        {
            if (mapperConfigurationFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperConfigurationFactory));
            }
            if (profiles == null)
            {
                throw new ArgumentNullException(nameof(profiles));
            }

            this.profiles = profiles.ToList();

            if (this.profiles.Count == 0)
            {
                throw new ArgumentException("Cannot instantiate MapperFactory without any mapping profiles.", nameof(profiles));
            }

            if (this.profiles.Any(p => p == null))
            {
                throw new ArgumentException("Cannot instantiate MapperFactory with null mapping profiles.", nameof(profiles));
            }

            var mapperConfiguration = mapperConfigurationFactory.CreateMapperConfiguration(this.profiles);
            mapper = mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Construct using a collection of specified mapping profiles.
        /// </summary>
        /// <param name="profiles">Mapping profiles to configure specific type mappers.</param>
        public MapperFactory(IEnumerable<Profile> profiles)
            : this(new MapperConfigurationFactory(), profiles)
        {
        }

        /// <summary>
        /// Construct using a custom, derived MapperConfigurationFactory instance and specified mapping profiles.
        /// </summary>
        /// <param name="mapperConfigurationFactory">Custom configuration factory for providing global configuration for all mappers.</param>
        /// <param name="profile">Mapping profile to configure specific type mapper.</param>
        /// <param name="profiles">Mapping profiles to configure specific type mappers.</param>
        public MapperFactory(MapperConfigurationFactory mapperConfigurationFactory, Profile profile, params Profile[] profiles)
            : this(mapperConfigurationFactory, new[] { profile }.Concat(profiles))
        {
        }

        /// <summary>
        /// Construct using specified mapping profiles.
        /// </summary>
        /// <param name="profile">Mapping profile to configure specific type mapper.</param>
        /// <param name="profiles">Mapping profiles to configure specific type mappers.</param>
        public MapperFactory(Profile profile, params Profile[] profiles)
            : this(new[] { profile }.Concat(profiles))
        {
        }

        /// <summary>
        /// Create a mapper instance for specific source and destination types.
        /// </summary>
        /// <typeparam name="TSource">Source type to map from.</typeparam>
        /// <typeparam name="TDestination">Destination type to map to.</typeparam>
        public IMapper<TSource, TDestination> Create<TSource, TDestination>()
        {
            List<IMappingProfile<TSource, TDestination>> matchingProfiles = profiles.OfType<IMappingProfile<TSource, TDestination>>().ToList();

            if (matchingProfiles.Count == 0)
            {
                throw new ArgumentException(
                    $"Could not find a matching mapping profile for source type '{typeof(TSource)}' and destination type '{typeof(TDestination)}'.");
            }

            if (matchingProfiles.Count > 1)
            {
                throw new ArgumentException(
                    $"Found multiple matching mapping profiles for source type '{typeof(TSource)}' and destination type '{typeof(TDestination)}'.");
            }

            return matchingProfiles[0].CreateMapper(mapper);
        }
    }
}