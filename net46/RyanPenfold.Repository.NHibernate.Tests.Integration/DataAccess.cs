// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   17 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Provides create / drop functionality for SQL Objects
    /// </summary>
    public class DataAccess
    {
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
                    sqlCommand.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IntegrationTests"].ConnectionString);
                    sqlCommand.CommandText = commandText;
                }

                // Open the connection of the "select ticket recipients" SQL command
                sqlCommand.Connection.Open();

                // Run the command
                result = sqlCommand.ExecuteScalar();
            }
            finally
            {
                // Close the connection for selectTicketRecipientsCommand if it//s instanitated and open 
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
