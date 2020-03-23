// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingConfigurationElementCollection.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Represents a configuration element containing a collection of child elements.
    /// </summary>
    /// <remarks>Ryan Penfold 23 April 2013</remarks>
    public class MappingConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new <see cref="ConfigurationElement"/> instance
        /// </summary>
        /// <returns>
        /// A newly created <see cref="ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MappingConfigurationElement();
        }

        /// <summary>
        /// Gets a key for a given <see cref="ConfigurationElement"/>
        /// </summary>
        /// <param name="element">
        /// The <see cref="ConfigurationElement"/> to get the key for
        /// </param>
        /// <returns>
        /// A key mapping to the <see cref="ConfigurationElement"/>
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return
                $"{((MappingConfigurationElement)element).AssemblyName}.{((MappingConfigurationElement)element).MappingType}";
        }
    }
}
