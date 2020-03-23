// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseClassMap.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentNHibernate.Mapping;

    using BusinessBase.Infrastructure;

    /// <summary>
    /// A base class for a class map.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="System.Type"/> to map.
    /// </typeparam>
    public class BaseClassMap<T> : ClassMap<T>, IClassMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClassMap{T}"/> class. 
        /// </summary>
        public BaseClassMap()
        {
            this.PrimaryKeyColumnNames = new List<string>();
        }

        /// <summary>
        /// Gets or sets the set of primary key column names.
        /// </summary>
        public IList<string> PrimaryKeyColumnNames { get; set; }

        /// <summary>
        /// Gets or sets the name of the schema the mapped table belongs to.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mapped table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Checks the database to see if a table exists with the specified name and schema.
        /// </summary>
        /// <returns>A <see cref="bool"/>.</returns>
        public void EnsureColumnExists(string columnName, string columnType)
        {
            this.EnsureColumnExists(this.SchemaName, this.TableName, columnName, columnType);
        }

        /// <summary>
        /// Checks the database to see if a table exists with the specified name and schema.
        /// </summary>
        /// <returns>A <see cref="bool"/>.</returns>
        /// <remarks>
        /// So far, this method only works with SQL Server databases
        /// </remarks>
        public void EnsureColumnExists(string tableSchema, string tableName, string columnName, string columnType)
        {
            // NULL-check the parameters
            if (string.IsNullOrWhiteSpace(tableSchema))
            {
                throw new ArgumentNullException(nameof(tableSchema));
            }

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException(nameof(columnName));
            }

            // TODO: Get this code working with a multitude of types of database
            if (!Configuration.Configuration.ConfigurationSection.PersistenceConfiguration.ToLower().StartsWith("mssqlconfiguration"))
            {
                return;
            }

            var columnExists = Convert.ToBoolean(Utilities.Data.SqlClient.SqlCommand.RunExecuteScalar(
                $"IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{tableSchema}' AND TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{columnName}')) SELECT 1 ELSE SELECT 0;",
                Configuration.Configuration.ConfigurationSection.ConnectionStringName));

            if (!columnExists)
            {
                Utilities.Data.SqlClient.SqlCommand.RunExecuteNonQuery(
                    $"ALTER TABLE [{tableSchema}].[{tableName}] ADD [{columnName}] {columnType};",
                    Configuration.Configuration.ConfigurationSection.ConnectionStringName);
            }
        }

        /// <summary>
        /// Checks the database to see if a schema exists with the specified name.
        /// </summary>
        /// <returns>A <see cref="bool"/>.</returns>
        public void EnsureSchemaExists()
        {
            this.EnsureSchemaExists(this.SchemaName);
        }

        /// <summary>
        /// Checks the database to see if a schema exists with the specified name.
        /// </summary>
        /// <remarks>
        /// So far, this method only works with SQL Server databases
        /// </remarks>
        public void EnsureSchemaExists(string name)
        {
            // NULL-check the parameters
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            // TODO: Get this code working with a multitude of types of database
            if (!Configuration.Configuration.ConfigurationSection.PersistenceConfiguration.ToLower().StartsWith("mssqlconfiguration"))
            {
                return;
            }

            var schemaExists = Convert.ToBoolean(Utilities.Data.SqlClient.SqlCommand.RunExecuteScalar(
                $"IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{name}')) SELECT 1 ELSE SELECT 0;",
                Configuration.Configuration.ConfigurationSection.ConnectionStringName));

            if (!schemaExists)
            {
                var sqlStatement = Utilities.Data.SqlClient.ScriptGenerator.GenerateCreateSchemaScript(name);
                //System.IO.File.WriteAllText(@"D:\Doug\Downloads\444\BLAH.txt", sqlStatement);
                Utilities.Data.SqlClient.SqlCommand.RunExecuteNonQuery(
                    sqlStatement,
                    Configuration.Configuration.ConfigurationSection.ConnectionStringName);
            }
        }

        /// <summary>
        /// Checks the database to see if a table exists with the specified name and schema.
        /// </summary>
        public void EnsureTableExists()
        {
            this.EnsureTableExists(typeof(T), this.SchemaName, this.TableName);
        }

        /// <summary>
        /// Checks the database to see if a table exists with the specified name and schema.
        /// </summary>
        /// <remarks>
        /// So far, this method only works with SQL Server databases
        /// </remarks>
        public void EnsureTableExists(Type mappedEntityType, string tableSchema, string tableName)
        {
            // NULL-check the parameters
            if (string.IsNullOrWhiteSpace(tableSchema))
            {
                throw new ArgumentNullException(nameof(tableSchema));
            }

            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            // TODO: Get this code working with a multitude of types of database
            if (!Configuration.Configuration.ConfigurationSection.PersistenceConfiguration.ToLower().StartsWith("mssqlconfiguration"))
            {
                return;
            }

            this.EnsureSchemaExists(tableSchema);

            var tableExists = Convert.ToBoolean(Utilities.Data.SqlClient.SqlCommand.RunExecuteScalar(
                $"IF (EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{tableSchema}' AND  TABLE_NAME = '{tableName}')) SELECT 1 ELSE SELECT 0;",
                Configuration.Configuration.ConfigurationSection.ConnectionStringName));

            if (!tableExists)
            {
                var sqlStatement = Utilities.Data.SqlClient.ScriptGenerator.GenerateCreateTableScript(mappedEntityType, tableName, tableSchema, false);
                //System.IO.File.WriteAllText(@"D:\Doug\Downloads\444\blah.txt", sqlStatement);
                Utilities.Data.SqlClient.SqlCommand.RunExecuteNonQuery(
                    sqlStatement,
                    Configuration.Configuration.ConfigurationSection.ConnectionStringName);
            }
        }

        /// <summary>
        /// Specify the identifier for this entity.
        /// </summary>
        public new IdentityPart Id(Expression<Func<T, object>> memberExpression)
        {
            var propertyName = memberExpression.Body is UnaryExpression
                                   // ReSharper disable PossibleNullReferenceException
                ? ((memberExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name
                                   // ReSharper restore PossibleNullReferenceException
                : (memberExpression.Body as MemberExpression)?.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new Exception("Unable to determine property name");
            }

            var columnType = Utilities.Data.SqlClient.SqlDbType.ToSqlDbTypeSqlString(typeof(T).GetProperty(propertyName).PropertyType);
            this.EnsureColumnExists(propertyName, columnType);
            return base.Id(memberExpression);
        }

        /// <summary>
        /// Specify the identifier for this entity.
        /// </summary>
        public new IdentityPart Id(Expression<Func<T, object>> memberExpression, string columnName)
        {
            var propertyName = memberExpression.Body is UnaryExpression
                                   // ReSharper disable PossibleNullReferenceException
                ? ((memberExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name
                                   // ReSharper restore PossibleNullReferenceException
                : (memberExpression.Body as MemberExpression)?.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new Exception("Unable to determine property name");
            }

            var columnType = Utilities.Data.SqlClient.SqlDbType.ToSqlDbTypeSqlString(typeof(T).GetProperty(propertyName).PropertyType);
            this.EnsureColumnExists(columnName, columnType);
            return base.Id(memberExpression, columnName);
        }

        /// <summary>
        /// Create a property mapping.
        /// </summary>
        /// <param name="memberExpression">Property to map</param>
        /// <returns>A <see cref="PropertyPart"/>.</returns>
        public new PropertyPart Map(Expression<Func<T, object>> memberExpression)
        {
            var propertyName = memberExpression.Body is UnaryExpression
                                   // ReSharper disable PossibleNullReferenceException
                ? ((memberExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name
                                   // ReSharper restore PossibleNullReferenceException
                : (memberExpression.Body as MemberExpression)?.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new Exception("Unable to determine property name");
            }

            var columnType = Utilities.Data.SqlClient.SqlDbType.ToSqlDbTypeSqlString(typeof(T).GetProperty(propertyName).PropertyType);
            this.EnsureColumnExists(propertyName, columnType);
            return base.Map(memberExpression);
        }

        /// <summary>
        /// Create a property mapping.
        /// </summary>
        /// <param name="memberExpression">Property to map</param>
        /// <param name="columnName">Property column name</param>
        /// <returns>A <see cref="PropertyPart"/>.</returns>
        public new PropertyPart Map(Expression<Func<T, object>> memberExpression, string columnName)
        {
            var propertyName = memberExpression.Body is UnaryExpression
                                   // ReSharper disable PossibleNullReferenceException
                ? ((memberExpression.Body as UnaryExpression).Operand as MemberExpression).Member.Name
                                   // ReSharper restore PossibleNullReferenceException
                : (memberExpression.Body as MemberExpression)?.Member.Name;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new Exception("Unable to determine property name");
            }

            var columnType = Utilities.Data.SqlClient.SqlDbType.ToSqlDbTypeSqlString(typeof(T).GetProperty(propertyName).PropertyType);
            this.EnsureColumnExists(columnName, columnType);
            return base.Map(memberExpression, columnName);
        }
    }
}
