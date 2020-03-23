// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   March 2020
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides create / drop functionality for SQL Objects
    /// </summary>
    [TestClass]
    public class DataAccess
    {
        private static string _connectionString;

        protected static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = RyanPenfold.Configuration.ConnectionStrings.Get("IntegrationTests");

                    if (string.IsNullOrEmpty(_connectionString))
                        throw new ConfigurationErrorsException(
                            "Connection string \"IntegrationTests\" not found.");
                }

                return _connectionString;

                //Repository.NHibernate.Configuration.Json.ConfigurationSection config1 = RyanPenfold.Configuration.SettingsProvider<Repository.NHibernate.Configuration.Json.ConfigurationSection>.GetSection("RyanPenfold.Repository.NHibernate");
                //IocContainer.Configuration.ConfigurationSettings config2 = RyanPenfold.Configuration.SettingsProvider<IocContainer.Configuration.ConfigurationSettings>.GetSection("RyanPenfold.IocContainer");}
            }
        }

        [AssemblyInitialize]
            public static void AssemblyInitialize(TestContext context)
        {
            RyanPenfold.Configuration.SettingsProvider.ConfigurationFileName = "appsettings.test.json";
        }

        /// <summary>
        /// Runs a SQL command from file
        /// </summary>
        /// <param name="fileName">The name of the file without the extension</param>
        /// <returns>The result of the database query</returns>
        public static object RunSqlCommandFromFile(string fileName)
        {
            return RunSqlCommand(System.IO.File.ReadAllText($"{System.IO.Directory.GetCurrentDirectory()}\\SQL\\{fileName}.sql"));
        }

        /// <summary>
        /// Runs a SQL command from file
        /// </summary>
        /// <param name="commandText">The text of the SQL command to run</param>
        /// <returns>The result of the database query</returns>
        public static object RunSqlCommand(string commandText)
        {
            // Result variable
            object result;

            // Sql command
            var sqlCommand = new SqlCommand();

            try
            {
                // Initialise the SQL command
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = new SqlConnection(ConnectionString);
                    sqlCommand.CommandText = commandText;
                }

                // Open the connection of the "select ticket recipients" SQL command
                sqlCommand.Connection.Open();

                // Run the command
                result = sqlCommand.ExecuteScalar();
            }
            finally
            {
                // Close the connection for the command if it's instantiated and open 
                if (sqlCommand.Connection != null && sqlCommand.Connection.State != ConnectionState.Closed)
                {
                    sqlCommand.Connection.Close();
                }
            }

            // Return the result
            return result;
        }
    }
}
