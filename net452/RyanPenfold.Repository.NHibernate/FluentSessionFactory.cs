// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentSessionFactory.cs" company="Ryan Penfold Ltd">
//   Copyright © Ryan Penfold Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;

    /// <summary>
    /// Fluent session factory
    /// </summary>
    public class FluentSessionFactory : SessionFactory, ISessionFactory
    {
        /// <summary>
        /// The _session factory.
        /// </summary>
        private static NHibernate.ISessionFactory sessionFactory;

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        private static NHibernate.ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    InitializeSessionFactory();
                }

                return sessionFactory;
            }
        }

        /// <summary>
        /// The get new session.
        /// </summary>
        /// <returns>
        /// The <see cref="ISession"/>.
        /// </returns>
        public new ISession GetNewSession()
        {
            return SessionFactory.OpenSession();
        }

        // TODO: Allow for connection string name, connection string, and mappings assembly
        // to be pulled from configuration settings.
        // Perhaps this class can be moved to RyanPenfold.Repository.NHibernate with a custom
        // configuration section that can switch between fluent or classic NHibernate.
        // Perhaps this class can tap in to the hibernate-configuration section
        // Both FluentSessionFactory and SessionFactory should be singletons stored in
        // a container
        // See this page for fluent configuration options
        // http://www.jagregory.com/writings/fluent-nhibernate-configuring-your-application/

        /// <summary>
        /// The initialize session factory.
        /// </summary>
        private static void InitializeSessionFactory()
        {
            sessionFactory =
                Fluently.Configure().Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(
                        @"Server=localhost;Database=RyanPenfold.SiteFoundation;Trusted_Connection=True;").ShowSql()).Mappings(
                            m => m.FluentMappings.AddFromAssembly(Assembly.Load("RyanPenfold.SiteFoundation.Repository"))).BuildSessionFactory();
        }
    }
}
