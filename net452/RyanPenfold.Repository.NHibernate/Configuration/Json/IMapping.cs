// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMapping.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration.Json
{
    /// <summary>
    /// Provides an interface for a mapping.
    /// </summary>
    public interface IMapping
    {
        /// <summary>
        /// Gets or sets the name of the assembly
        /// </summary>
        string Assembly { get; set; }

        /// <summary>
        /// Gets or sets the mapping type
        /// </summary>
        string Type { get; set; }
    }
}
