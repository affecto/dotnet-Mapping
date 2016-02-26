using System;
using System.Configuration;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
   
    /// <summary>
    /// Abstract base class for configuring AutoMapper maps statically.
    /// </summary>
    /// <typeparam name="T">Type parameter is used only to force static members to be recreated per subtype.</typeparam>
    public abstract class AutoMapConfigurator<T> where T : AutoMapConfigurator<T>
    {
        private static readonly object createLock = new object();
        private static bool mapsCreated;
        private static bool? disableConfigurationValidationOnInitialize;

        private static bool DisableConfigurationValidationOnInitialize
        {
            get
            {
                if (!disableConfigurationValidationOnInitialize.HasValue)
                {               
                    string str = ConfigurationManager.AppSettings["DisableAutomapperConfigurationValidation"];
                    disableConfigurationValidationOnInitialize = (!string.IsNullOrEmpty(str) && str.Equals("false", StringComparison.InvariantCultureIgnoreCase));
                }

                return disableConfigurationValidationOnInitialize.Value;
            }
        }

        protected AutoMapConfigurator()
            : this(true)
        {
        }

        protected AutoMapConfigurator(bool initializeMaps)
        {
            if (initializeMaps)
            {
                Initialize();
            }
        }

        /// <summary>
        /// Abstract method for defining auto maps. Use static Mapper class to define maps.
        /// </summary>
        protected abstract void ConfigureMaps();

        /// <summary>
        /// Initializes once the auto maps defined in ConfigureMaps() method.
        /// </summary>
        private void Initialize()
        {
            lock (createLock)
            {
                if (!mapsCreated)
                {
                    ConfigureMaps();
                    mapsCreated = true;
                    AssertConfigurationIsValid();
                }
            }
        }

        private static void AssertConfigurationIsValid()
        {
            if (!DisableConfigurationValidationOnInitialize)
            {
                Mapper.AssertConfigurationIsValid();
            }
        }
    }
}