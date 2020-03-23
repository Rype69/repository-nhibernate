// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    using System;
    using System.Configuration;
    using System.Reflection;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Repository.NHibernate;

    /// <summary>
    /// Allows the application to specify properties and mapping 
    /// documents to be used when creating a 
    /// <see cref="NHibernate.ISessionFactory" /> object.
    /// </summary>
    public class Configuration : IConfiguration
    {
        /// <summary>
        /// Gets or sets the strongly typed configuration section data.
        /// </summary>
        public static MappingsConfigurationSection ConfigurationSection
        {
            get
            {
                var assemblyName = MethodBase.GetCurrentMethod()?.DeclaringType?.Assembly.GetName().Name;

                // Backwards compatibility - previously config sections were labelled "nhibernateRepository"
                var possibleConfigurationSectionNames = new[] { assemblyName, "nhibernateRepository" };

                MappingsConfigurationSection result;
                var count = 0;
                do
                {
                    result = ConfigurationManager.GetSection(possibleConfigurationSectionNames[count]) as MappingsConfigurationSection;
                    count++;
                } while (result == null && count < possibleConfigurationSectionNames.Length);

                return result;
            }
        }

        /// <summary>
        /// Instantiate a new NHibernate.ISessionFactory, 
        /// using the properties and mappings in this configuration. 
        /// The NHibernate.ISessionFactory will be immutable, 
        /// so changes made to the configuration after building 
        /// the NHibernate.ISessionFactory will not affect it.
        /// </summary>
        /// <returns>
        /// A session factory
        /// </returns>
        /// <remarks>Ryan Penfold 23 April 2013</remarks>
        public NHibernate.ISessionFactory BuildSessionFactory()
        {
            // If the configuration section is not present in the config file, throw an exception
            if (ConfigurationSection == null)
            {
                throw new ConfigurationErrorsException("Mappings configuration section not found");
            }

            // If the connection string name property is null or empty, throw an exception
            if (string.IsNullOrWhiteSpace(ConfigurationSection.ConnectionStringName))
            {
                throw new ConfigurationErrorsException("Connection string name not found in configuration settings");
            }

            // Attempt to find the specified connection string in the configuration settings
            var connectionStringConfig = ConfigurationManager.ConnectionStrings[ConfigurationSection.ConnectionStringName];

            // If the connection string is not present, or is empty, throw an exception
            if (string.IsNullOrWhiteSpace(connectionStringConfig?.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    $"Connection string \"{ConfigurationSection.ConnectionStringName}\" not found in the configuration settings");
            }

            // If there isn't a PersistenceConfiguration specified, throw an exception
            if (string.IsNullOrWhiteSpace(ConfigurationSection.PersistenceConfiguration))
            {
                throw new ConfigurationErrorsException("No PersistenceConfiguration specified");
            }

            FluentConfiguration = this.FluentlyConfigure(ConfigurationSection.ConnectionStringName, ConfigurationSection.PersistenceConfiguration, ConfigurationSection.Dialect);

            // For each "mapping" element in the config
            foreach (MappingConfigurationElement mappingConfigElement in ConfigurationSection.Mappings)
            {
                // If, for whatever reason, the mappingConfigElement is nothing then skip this element
                if (mappingConfigElement == null)
                {
                    continue;
                }

                // Declare "type" variable to denote the mapping type - the default value is "Hbm"
                var type = MappingType.Hbm;

                // If the mapping type element is not null or empty, attempt to parse te value as a valid mapping type
                if (!string.IsNullOrWhiteSpace(mappingConfigElement.MappingType) && !Enum.TryParse(mappingConfigElement.MappingType, out type))
                {
                    throw new FormatException($"Invalid mapping type \"{mappingConfigElement.MappingType}\".");
                }

                // Attempt to load the specified mapping
                var assembly = Assembly.Load(mappingConfigElement.AssemblyName);

                // If the assembly is not loaded, throw an exception
                if (assembly == null)
                {
                    throw new Exception($"Assembly {mappingConfigElement.AssemblyName} not found");
                }

                // Depending on the required mapping type, call the appropriate "add mappings" method
                switch (type)
                {
                    case MappingType.Auto:

                        FluentConfiguration.Mappings(m => m.AutoMappings.Add(AutoMap.Assembly(assembly)));
                        break;

                    case MappingType.Fluent:

                        FluentConfiguration.Mappings(m => m.FluentMappings.AddFromAssembly(assembly));
                        break;

                    case MappingType.Hbm:

                        FluentConfiguration.Mappings(m => m.HbmMappings.AddFromAssembly(assembly));
                        break;
                }
            }

            // Return the session factory
            return FluentConfiguration.BuildSessionFactory();
        }

        /// <summary>
        /// Gets or sets an <see cref="FluentConfiguration"/> instance.
        /// </summary>
        public static FluentConfiguration FluentConfiguration { get; set; }

        /// <summary>
        /// Configures the NHibernate
        /// </summary>
        public void Configure()
        {
        }

        /// <summary>
        /// Creates a <see cref="FluentConfiguration"/> instance
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string</param>
        /// <param name="persistenceConfiguration">The persistence configuration type name</param>
        /// <param name="dialect">The persistence configuration dialect full type name</param>
        /// <returns>An <see cref="FluentConfiguration"/> instance</returns>
        private FluentConfiguration FluentlyConfigure(string connectionStringName, string persistenceConfiguration, string dialect = null)
        {
            if (connectionStringName == null)
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            if (persistenceConfiguration == null)
            {
                throw new ArgumentNullException(nameof(persistenceConfiguration));
            }

            if (string.IsNullOrWhiteSpace(persistenceConfiguration) || persistenceConfiguration.IndexOf(".", StringComparison.Ordinal) == -1
                || persistenceConfiguration.IndexOf(".", StringComparison.Ordinal) < 1)
            {
                throw new FormatException($"Persistence configuration is of invalid format {persistenceConfiguration}");
            }

            var assembly = Assembly.Load("FluentNHibernate");

            if (assembly == null)
            {
                throw new Exception("Assembly FluentNHibernate not found.");
            }

            var configurationType =
                assembly.GetType(
                    $"FluentNHibernate.Cfg.Db.{persistenceConfiguration.Substring(0, persistenceConfiguration.IndexOf(".", StringComparison.Ordinal))}");

            if (configurationType == null)
            {
                throw new Exception($"Type {persistenceConfiguration} not found.");
            }

            var configurationPropertyName =
                persistenceConfiguration.Substring(persistenceConfiguration.IndexOf(".", StringComparison.Ordinal) + 1);

            var configurationProperty = configurationType.GetProperty(configurationPropertyName);

            if (configurationProperty == null)
            {
                throw new Exception(
                    $"Property with name {configurationPropertyName} not found on type {configurationType.Name}.");
            }

            var configurationPropertyValue = configurationProperty.GetValue(configurationType, null);

            if (configurationPropertyValue == null)
            {
                throw new Exception(
                    $"Property with name {configurationProperty} on type {configurationType} has no value.");
            }

            Type dialectType = null;
            if (!string.IsNullOrWhiteSpace(dialect))
            {
                dialectType = Type.GetType(dialect);
                if (dialectType == null)
                {
                    throw new TypeLoadException($"Cannot load dialect type {dialect}");
                }
            }

            if (dialectType != null)
            {
                configurationPropertyValue =
                    configurationPropertyValue.GetType().GetMethod("Dialect", new Type[] { }).MakeGenericMethod(dialectType).Invoke(configurationPropertyValue, new object[] { });
            }

            if (configurationPropertyValue == null)
            {
                throw new Exception($"Cannot load dialect {dialect}");
            }

            var configurationPropertyType = configurationPropertyValue.GetType();
            var connectionStringMethod = configurationPropertyType.GetMethod("ConnectionString", new[] { typeof(string) });

            if (connectionStringMethod == null)
            {
                throw new Exception($"ConnectionString method not found on type {configurationPropertyType}.");
            }

            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionStringSettings == null)
            {
                throw new Exception($"Connection string {connectionStringName} not found in the configuration settings");
            }

            if (string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new Exception($"Connection string {connectionStringName} ");
            }

            return Fluently.Configure().Database((IPersistenceConfigurer)connectionStringMethod.Invoke(configurationPropertyValue, new object[] { connectionStringSettings.ConnectionString }));
        }
    }
}
