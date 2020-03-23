// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfiguration.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Configuration
{
    /// <summary>
    /// Allows the application to specify properties and mapping 
    /// documents to be used when creating a 
    /// <see cref="NHibernate.ISessionFactory" /> object.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Configures the NHibernate
        /// </summary>
        void Configure();

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
        NHibernate.ISessionFactory BuildSessionFactory();
    }
}