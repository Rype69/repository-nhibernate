// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mapping.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration.Json
{
    /// <summary>
    /// A mapping.
    /// </summary>
    public class Mapping : IMapping
    {
        /// <summary>
        /// Gets or sets the name of the assembly
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// Gets or sets the mapping type
        /// </summary>
        public string Type { get; set; }
    }
}
