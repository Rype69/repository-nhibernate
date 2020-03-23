// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISessionFactory.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    /// <summary>
    /// Provides an interface for functionality that creates NHibernate sessions
    /// </summary>
    public interface ISessionFactory
    {
        /// <summary>
        /// Creates and returns a new NHibernate session
        /// </summary>
        /// <returns>
        /// An ISession object
        /// </returns>
        global::NHibernate.ISession GetNewSession();
    }
}
