// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationSection.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration.Json
{
    using System.Collections.Generic;

    /// <summary>
    /// A strongly typed set of configuration settings read from in a JSON file.
    /// </summary>
    public class ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the connection string name specified in the configuration file
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets the dialect specified in the configuration file
        /// </summary>
        public string Dialect { get; set; }

        /// <summary>
        /// Gets or sets the mappings collection specified in the configuration file
        /// </summary>
        public IEnumerable<Mapping> Mappings { get; set; }

        /// <summary>
        /// Gets or sets the persistence configuration type specified in the configuration file
        /// </summary>
        public string PersistenceConfiguration { get; set; }
    }
}
