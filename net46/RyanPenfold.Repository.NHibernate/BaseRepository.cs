// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   11 September 2012 19:12
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using BusinessBase.Infrastructure;

    using Configuration;

    using NHibernate.Criterion;

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to store
    /// </typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class. 
        /// </summary>
        /// <param name="sessionFactory">
        /// The session Factory.
        /// </param>
        /// <param name="typeMap">
        /// Mapping information for type T
        /// </param>
        protected BaseRepository(ISessionFactory sessionFactory, BaseClassMap<T> typeMap)
        {
            // NULL-check parameters
            if (sessionFactory == null)
            {
                throw new ArgumentNullException(nameof(sessionFactory));
            }

            if (typeMap == null)
            {
                throw new ArgumentNullException(nameof(typeMap));
            }

            // Set the field values
            this.SessionFactory = sessionFactory;
            this.TypeMap = typeMap;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class. 
        /// </summary>
        protected BaseRepository(BaseClassMap<T> typeMap)
            : this(IocContainer.Resolver.Resolve<ISessionFactory>(), typeMap)
        {
        }

        /// <summary>
        /// Gets or sets the mapping information.
        /// </summary>
        public IClassMap TypeMap { get; set; } 

        /// <summary>
        /// Gets or sets the DefaultSortField.
        /// </summary>
        public string DefaultSortField { get; set; }

        /// <summary>
        /// Gets or sets the session factory
        /// </summary>
        protected ISessionFactory SessionFactory { get; set; }

        /// <summary>
        /// Counts the amount of instances of type <see cref="T"/> in a data store
        /// </summary>
        /// <returns>
        /// The amount of instances of type <see cref="T"/> in a data store
        /// </returns>
        public virtual long Count()
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                var query = session.CreateQuery($"SELECT COUNT(*) from {typeof(T)}");
                return Convert.ToInt64(query.UniqueResult());
            }
        }

        /// <summary>
        /// Finds all the objects of type T in a store
        /// </summary>
        /// <returns>
        /// An IList of objects of type T
        /// </returns>
        public virtual IList<T> FindAll()
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                var query = session.CreateCriteria(typeof(T));

                if (!string.IsNullOrEmpty(this.DefaultSortField))
                {
                    query.AddOrder(new Order(this.DefaultSortField, true));
                }

                return query.List<T>();
            }
        }

        /// <summary>
        /// Attempts to find a <see cref="T"/> instance with the given identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the object to search for
        /// </param>
        /// <returns>
        /// A <see cref="T"/> if found, otherwise null
        /// </returns>
        public virtual T FindById(object id)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                return session.Get<T>(id);
            }
        }

        /// <summary>
        /// Attempts to find a set of <see cref="T"/> instances with the given identifiers.
        /// </summary>
        /// <param name="ids">
        /// The identifiers of the objects to find
        /// </param>
        /// <returns>
        /// A <see cref="IList{T}"/>
        /// </returns>
        public virtual IList<T> FindByIds(object[] ids)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                return session.CreateCriteria<T>()
                        .Add(Restrictions.In("Id", ids))
                        .List<T>();
            }
        }

        /// <summary>
        /// Finds the maximum value of the specified column name.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of value to find.
        /// </typeparam>
        /// <param name="columnName">
        /// The name of the column to find the maximum value of.
        /// </param>
        /// <returns>
        /// An instance of type <see cref="TValue"/>.
        /// </returns>
        public virtual TValue FindMaxValue<TValue>(string columnName)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                var criteria = session.CreateCriteria<T>();
                criteria.SetProjection(Projections.Max(columnName));
                return criteria.UniqueResult<TValue>();
            }
        }

        /// <summary>
        /// Generates a new unique identifier for the mapped type
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property or column to generate a new identifier for
        /// </param>
        /// <returns>
        /// A new unique identifier for the mapped type
        /// </returns>
        public virtual string NewId(string propertyName)
        {
            // TODO: Make this a generic method?

            // Result variable
            string newIdentifier;

            // Name and schema name of a stored procedure
            var procedureName = "GenerateNewId";
            var procedureSchema = "Foundation";

            // Build the text of a command that determines whether the aforenamed procedure exists in the database.
            var commandText = new StringBuilder();
            commandText.AppendLine("SELECT");
            commandText.AppendLine("    COUNT(1) ");
            commandText.AppendLine("FROM");
            commandText.AppendLine("    [sys].[objects] ");
            commandText.AppendLine("INNER JOIN ");
            commandText.AppendLine("    [sys].[schemas] ");
            commandText.AppendLine("    ON ");
            commandText.AppendLine("    [sys].[objects].[schema_id] = [sys].[schemas].[schema_id] ");
            commandText.AppendLine("WHERE ");
            commandText.AppendLine("    [sys].[objects].[type_desc] = 'SQL_STORED_PROCEDURE'");
            commandText.AppendLine("    AND");
            commandText.AppendLine($"    [sys].[objects].[name] = '{procedureName}'");
            commandText.AppendLine("    AND");
            commandText.AppendLine($"    [sys].[schemas].[name] = '{procedureSchema}';");

            // Configuration section has information about the connection string
            var configurationSection = ConfigurationManager.GetSection("nhibernateRepository") as MappingsConfigurationSection;

            // Flag shows whether or not the required stored procedure exists
            bool storedProcedureExists;

            // Sql command 
            SqlCommand sqlCommand = null;

            // Sql connection
            SqlConnection sqlconnection;

            try
            {
                // Determine whether the aforenamed procedure exists in the database.
                if (string.IsNullOrWhiteSpace(configurationSection?.ConnectionStringName))
                {
                    throw new Exception("Unable to determine ConnectionStringName");
                }

                sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings[configurationSection.ConnectionStringName].ConnectionString);
                sqlCommand = new SqlCommand(commandText.ToString(), sqlconnection);
                sqlCommand.Connection.Open();
                var result = (int)sqlCommand.ExecuteScalar();
                sqlCommand.Connection.Close();
                storedProcedureExists = result >= 1;
            }
            finally
            {
                // Close connection if it's open 
                if (sqlCommand?.Connection.State == ConnectionState.Open)
                {
                    sqlCommand.Connection.Close();
                }
            }

            // If the procedure exists, run it to obtain a new ID straight from the database. If not, generate a new GUID.
            if (storedProcedureExists)
            {
                // Instantiate a new database connection
                sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings[configurationSection.ConnectionStringName].ConnectionString);

                // Build SQL command
                sqlCommand = new SqlCommand($"[{procedureSchema}].[{procedureName}]", sqlconnection) { CommandType = CommandType.StoredProcedure };

                // Add parameter values
                sqlCommand.Parameters.AddWithValue("@TableSchema", this.TypeMap.SchemaName);
                sqlCommand.Parameters.AddWithValue("@TableName", this.TypeMap.TableName);
                sqlCommand.Parameters.AddWithValue("@ColumnName", propertyName);
                sqlCommand.Parameters.Add("@Result", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                // Open SQL connection
                sqlCommand.Connection.Open();

                // Run SQL command
                sqlCommand.ExecuteNonQuery();

                // Get the result value
                newIdentifier = Convert.ToString(sqlCommand.Parameters["@Result"].Value);

                // Close SQL connection
                sqlCommand.Connection.Close();
            }
            else
            {
                // Attempt to get the property info for the type
                var propertyInfo = typeof(T).GetProperty(propertyName);

                // NULL-check the property info
                if (propertyInfo == null)
                {
                    throw new MissingMemberException(typeof(T).Name, propertyName);
                }

                // Create new NHibernate session
                using (var session = this.SessionFactory.GetNewSession())
                {
                    // Get all the entities of this type from the data store
                    var allEntities = session.CreateCriteria<T>().List<T>();

                    // Generate a unique identifier for the new entity
                    do
                    {
                        newIdentifier = Guid.NewGuid().ToString();
                    }
                    while (allEntities.Any(e => string.Equals(propertyInfo.GetValue(e) as string, newIdentifier, StringComparison.InvariantCultureIgnoreCase)));
                }
            }

            // Return result
            return newIdentifier;
        }

        /// <summary>
        /// Removes an entity from a store
        /// </summary>
        /// <param name="entity">
        /// The entity to remove.
        /// </param>
        public virtual void Remove(T entity)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Searches for an entity with the given identifier and removes it from a store
        /// </summary>
        /// <param name="id">
        /// The identifier of the entity to remove.
        /// </param>
        public virtual void RemoveBy(long id)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.CreateQuery($"DELETE {typeof(T)} WHERE id = :id")
                           .SetParameter("id", id)
                           .ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Saves an object to a store
        /// </summary>
        /// <param name="entity">
        /// The entity to save.
        /// </param>
        public virtual void Save(T entity)
        {
            using (var session = this.SessionFactory.GetNewSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();
                }
            }
        }
    }
}