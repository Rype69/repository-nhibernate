// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingsConfigurationSection.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   23 April 2013 09:17
// </date>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using NHibernate.Mapping;
using RyanPenfold.Repository.NHibernate.Configuration.Json;

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    using System;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Represents a section within a configuration file.
    /// </summary>
    /// <remarks>Ryan Penfold 23 April 2013 09:17</remarks>
    public class MappingsConfigurationSection : ConfigurationSection, IConfigurationSection
    {
        /// <summary>
        /// The configuration tag name.
        /// </summary>
        public const string ConnectionStringNameAttributeName = "connectionStringName";

        /// <summary>
        /// The dialect attribute name.
        /// </summary>
        public const string DialectAttributeName = "dialect";

        /// <summary>
        /// The only application-wide instance of this type.
        /// </summary>
        /// <remarks>
        /// Singleton pattern
        /// </remarks>
        private static IConfigurationSection instance;

        /// <summary>
        /// The mappings tag name.
        /// </summary>
        public const string MappingsAttributeName = "mappings";

        /// <summary>
        /// The persistence configuration attribute name.
        /// </summary>
        public const string PersistenceConfigurationAttributeName = "persistenceConfiguration";

        /// <summary>
        /// Gets or sets the connection string name specified in the configuration file
        /// </summary>
        [ConfigurationProperty(ConnectionStringNameAttributeName, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return this[ConnectionStringNameAttributeName] as string; }
            set { this[ConnectionStringNameAttributeName] = value; }
        }

        /// <summary>
        /// Gets or sets the dialect specified in the configuration file
        /// </summary>
        [ConfigurationProperty(DialectAttributeName, IsRequired = false)]
        public string Dialect
        {
            get { return this[DialectAttributeName] as string; }
            set { this[DialectAttributeName] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not Repository.NHibernate is enabled.
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return Convert.ToBoolean(this["enabled"]);
            }

            set
            {
                this["enabled"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the only application-wide instance of this type.
        /// </summary>
        public static IConfigurationSection Instance => instance ?? (instance = ConfigurationManager.GetSection("nhibernateRepository") as IConfigurationSection);

        /// <summary>
        /// Gets or sets the mappings collection specified in the configuration file
        /// </summary>
        [ConfigurationProperty(MappingsAttributeName, IsRequired = true)]
        public MappingConfigurationElementCollection Mappings
        {
            get { return this[MappingsAttributeName] as MappingConfigurationElementCollection; }
            set { this[MappingsAttributeName] = value; }
        }

        /// <inheritdoc />
        [ConfigurationProperty(PersistenceConfigurationAttributeName, IsRequired = true)]
        public string PersistenceConfiguration
        {
            get { return this[PersistenceConfigurationAttributeName] as string; }
            set { this[PersistenceConfigurationAttributeName] = value; }
        }

        /// <inheritdoc />
        public IEnumerable<Mapping> GetMappings()
        {
            var rtn = new List<Mapping>();
            foreach (MappingConfigurationElement mapping in Mappings)
            {
                rtn.Add(new Mapping { Assembly = mapping.AssemblyName, Type = mapping.MappingType });
            }

            return rtn;
        }
    }
}
