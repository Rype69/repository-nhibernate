// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingConfigurationElement.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Represents a mapping configuration element in a configuration file
    /// </summary>
    /// <remarks>Ryan Penfold 23 April 2013</remarks>
    public class MappingConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// The name of the assembly property
        /// </summary>
        public const string AssemblyPropertyName = "assembly";

        /// <summary>
        /// The name of the type property
        /// </summary>
        public const string TypePropertyName = "type";

        /// <summary>
        /// Gets or sets the name of the assembly
        /// </summary>
        [ConfigurationProperty(AssemblyPropertyName, IsRequired = true, IsKey = false)]
        public string AssemblyName
        {
            get { return this[AssemblyPropertyName] as string; }
            set { this[AssemblyPropertyName] = value; }
        }

        /// <summary>
        /// Gets or sets the mapping type
        /// </summary>
        [ConfigurationProperty(TypePropertyName, IsRequired = false, IsKey = false)]
        public string MappingType
        {
            get { return this[TypePropertyName] as string; }
            set { this[TypePropertyName] = value; }
        }
    }
}