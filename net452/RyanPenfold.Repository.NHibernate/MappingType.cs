// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingType.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    /// <summary>
    /// Denotes a type of mapping 
    /// </summary>
    public enum MappingType
    {
        /// <summary>
        /// Denotes an automatic mapping type
        /// </summary>
        Auto,

        /// <summary>
        /// Denotes a fluent mapping type
        /// </summary>
        Fluent,

        /// <summary>
        /// Denotes a HBM mapping type
        /// </summary>
        Hbm
    }
}
