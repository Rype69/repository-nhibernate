// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigurationSection.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration.Json
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides an interface for a strongly typed set of configuration settings read from a JSON file.
    /// </summary>
    public interface IConfigurationSection
    {
        /// <summary>
        /// Gets or sets the connection string name specified in the configuration file
        /// </summary>
        string ConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets the dialect specified in the configuration file
        /// </summary>
        string Dialect { get; set; }

        /// <summary>
        /// Gets or sets the mappings collection specified in the configuration file
        /// </summary>
        IEnumerable<Mapping> Mappings { get; set; }

        /// <summary>
        /// Gets or sets the persistence configuration type specified in the configuration file
        /// </summary>
        string PersistenceConfiguration { get; set; }
    }
}
