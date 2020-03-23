// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionFactory.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    using Configuration;

    using NHibernate;

    /// <summary>
    /// Creates NHibernate sessions
    /// </summary>
    public class SessionFactory : ISessionFactory
    {
        /// <summary>
        /// Gets or sets the SessionFactory.
        /// </summary>
        private readonly NHibernate.ISessionFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactory"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public SessionFactory(IConfiguration configuration)
        {
            configuration.Configure();

            // Ensure mapping files are embedded in assembly
            this.factory = configuration.BuildSessionFactory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionFactory"/> class.
        /// </summary>
        public SessionFactory()
            : this(IocContainer.Resolver.Resolve<IConfiguration>())
        {
        }

        /*
        /// <summary>
        /// Get the <see cref="IClassMetadata"/> associated with the given entity name.
        /// </summary>
        /// <param name="entityName">The name of the entity.</param>
        /// <returns>An <see cref="IClassMetadata"/>.</returns>
        public IClassMetadata GetClassMetadata(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }
        }*/

        /// <summary>
        /// Creates and returns a new NHibernate session
        /// </summary>
        /// <returns>
        /// An ISession object
        /// </returns>
        public ISession GetNewSession()
        {
            return this.factory.OpenSession();
        }
    }
}
