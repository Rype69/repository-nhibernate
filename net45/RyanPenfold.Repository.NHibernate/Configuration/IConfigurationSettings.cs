// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMappingsConfigurationSection.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   30 April 2018 15:47
// </date>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using RyanPenfold.Repository.NHibernate.Configuration.Json;

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    /// <summary>
    /// Provides an interface for a section within a configuration file.
    /// </summary>
    /// <remarks>Ryan Penfold 23 April 2013 09:17</remarks>
    public interface IConfigurationSettings
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
        /// Gets or sets a value indicating whether or not database access is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the collection of mapping configuration elements
        /// </summary>
        /// <remarks>Ryan Penfold 30 April 2018</remarks>
        IEnumerable<Mapping> Mappings { get; set; }

        /// <summary>
        /// Gets or sets the persistence configuration type specified in the configuration file
        /// </summary>
        string PersistenceConfiguration { get; set; }
    }
}
