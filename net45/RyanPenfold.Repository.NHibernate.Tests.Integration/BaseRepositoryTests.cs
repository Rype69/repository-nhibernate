// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRepositoryTests.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   16 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    using System;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides integration tests for the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
    /// </summary>
    [TestClass]
    public class BaseRepositoryTests
    {
        /// <summary>
        /// Tests the Count method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void Count_ObjectsToCount_CorrectQuantityComputed()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Insert a random amount of records into the table
                for (var i = 0; i < new Random().Next(1, 10); i++)
                {
                    // Insert into the table
                    DataAccess.RunSqlCommand(
                        "INSERT INTO [EmailTemplate] ([EmailName],[EmailBody]) VALUES ('New Order Placed','We have recieved your order. It will be processed shortly.');SELECT SCOPE_IDENTITY()");
                }

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Act
                var result = repository.Count();

                // Select count from table, it should be equal to the result
                object count = Convert.ToInt64(DataAccess.RunSqlCommand("SELECT COUNT(*) FROM [EmailTemplate];"));
                Assert.AreEqual(result, count);
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }

        /// <summary>
        /// Tests the FindAll method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void FindAll_NoParameters_FindsAllObjects()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Get list of results
                var results = repository.FindAll();

                // Assert that the results is instantiated
                Assert.IsNotNull(results);

                // Assert that the results contains zero objects
                Assert.AreEqual(0, results.Count);

                // Insert into the table
                DataAccess.RunSqlCommand(
                    "INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('New Order Placed','We have recieved your order. It will be processed shortly.')");
                DataAccess.RunSqlCommand(
                    "INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('Order Shipped','We have dispatched your order.')");

                // Re-select all the data
                results = repository.FindAll();

                // Assert that the results is instantiated
                Assert.IsNotNull(results);

                // Assert that the results contains zero objects
                Assert.AreEqual(2, results.Count);

                // Check the values of item 0
                var result0 = results[0];
                Assert.IsNotNull(result0);
                Assert.AreEqual("New Order Placed", result0.EmailName);
                Assert.AreEqual("We have recieved your order. It will be processed shortly.", result0.EmailBody);

                // Check the values of item 1
                var result1 = results[1];
                Assert.IsNotNull(result1);
                Assert.AreEqual("Order Shipped", result1.EmailName);
                Assert.AreEqual("We have dispatched your order.", result1.EmailBody);

                // Delete all the data
                DataAccess.RunSqlCommand("DELETE FROM [IntegrationTests].[dbo].[EmailTemplate]");

                // Re-select all the data, the table should be empty now
                results = repository.FindAll();

                // Assert that the results contains zero objects
                Assert.AreEqual(0, results.Count);
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }

        /// <summary>
        /// Tests the FindBy method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void FindBy_GivenID_ObjectFound()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Insert into the table
                object id0 =
                    DataAccess.RunSqlCommand("INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('New Order Placed','We have recieved your order. It will be processed shortly.');SELECT SCOPE_IDENTITY()");
                object id1 =
                    DataAccess.RunSqlCommand("INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('Order Shipped','We have dispatched your order.');SELECT SCOPE_IDENTITY()");

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Attempt to find a couple of objects
                // Check the values of item 0
                var result0 = repository.FindById(Convert.ToInt64(id0));
                Assert.IsNotNull(result0);
                Assert.AreEqual("New Order Placed", result0.EmailName);
                Assert.AreEqual("We have recieved your order. It will be processed shortly.", result0.EmailBody);

                // Check the values of item 1
                var result1 = repository.FindById(Convert.ToInt64(id1));
                Assert.IsNotNull(result1);
                Assert.AreEqual("Order Shipped", result1.EmailName);
                Assert.AreEqual("We have dispatched your order.", result1.EmailBody);
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }

        /// <summary>
        /// Tests the <see cref="BaseRepository{T}.NewId"/> method.
        /// </summary>
        [TestMethod]
        public void NewId_NoParameters_NewIdReceived()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_CONTAINSGUID");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_CONTAINSGUID");

                // Build the insert command text
                var insertCommandTextBuilder = new StringBuilder();
                insertCommandTextBuilder.AppendLine("DECLARE @Count BIGINT; ");
                insertCommandTextBuilder.AppendLine("SET @Count = 0; ");
                insertCommandTextBuilder.AppendLine("WHILE @Count < 10 ");
                insertCommandTextBuilder.AppendLine("BEGIN");
                insertCommandTextBuilder.AppendLine("   INSERT INTO [dbo].[ContainsGuid] ([Id]) VALUES (NEWID());");
                insertCommandTextBuilder.AppendLine("   SET @Count = @Count + 1; ");
                insertCommandTextBuilder.AppendLine("END");

                // Run the insert command 
                DataAccess.RunSqlCommand(insertCommandTextBuilder.ToString());

                // Create new repository
                var repository = new ContainsGuidRepository();

                // Select all data
                var allData = repository.FindAll();

                // Generate new id
                var newId = repository.NewId("Id");

                // Assert that the new id is unique
                Assert.IsFalse(allData.Any(
                    a => string.Equals(a.Id.ToString(), newId.ToString(), StringComparison.InvariantCultureIgnoreCase)));
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_CONTAINSGUID");
            }
        }

        /// <summary>
        /// Tests the Remove method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void Remove_ObjectToRemove_Removed()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Insert into the table
                object id0 =
                    DataAccess.RunSqlCommand(
                        "INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('New Order Placed','We have recieved your order. It will be processed shortly.');SELECT SCOPE_IDENTITY()");
                object id1 =
                    DataAccess.RunSqlCommand("INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('Order Shipped','We have dispatched your order.');SELECT SCOPE_IDENTITY()");

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Attempt to find a couple of objects
                // Check the values of item 0
                var result0 = repository.FindById(Convert.ToInt64(id0));
                Assert.IsNotNull(result0);
                Assert.AreEqual("New Order Placed", result0.EmailName);
                Assert.AreEqual("We have recieved your order. It will be processed shortly.", result0.EmailBody);

                // Check the values of item 1
                var result1 = repository.FindById(Convert.ToInt64(id1));
                Assert.IsNotNull(result1);
                Assert.AreEqual("Order Shipped", result1.EmailName);
                Assert.AreEqual("We have dispatched your order.", result1.EmailBody);

                // Attempt to remove
                repository.Remove(result0);

                // Select count from table, it should be one
                object count = DataAccess.RunSqlCommand("SELECT COUNT(*) FROM [EmailTemplate];");
                Assert.AreEqual(1, count);

                // Select top identifier from table, it should be equal to the value in the id1 variable
                object id = Convert.ToDecimal(DataAccess.RunSqlCommand("SELECT TOP 1 [id] FROM [EmailTemplate];"));
                Assert.AreEqual(id1, id);
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }

        /// <summary>
        /// Tests the RemoveBy method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void RemoveBy_ObjectToRemove_Removed()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Insert into the table
                var id0 =
                    DataAccess.RunSqlCommand(
                        "INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('New Order Placed','We have recieved your order. It will be processed shortly.');SELECT SCOPE_IDENTITY()");
                var id1 =
                    DataAccess.RunSqlCommand(
                        "INSERT INTO [EmailTemplate] ([emailName],[emailBody]) VALUES ('Order Shipped','We have dispatched your order.');SELECT SCOPE_IDENTITY()");

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Attempt to find a couple of objects
                // Check the values of item 0
                var result0 = repository.FindById(Convert.ToInt64(id0));
                Assert.IsNotNull(result0);
                Assert.AreEqual("New Order Placed", result0.EmailName);
                Assert.AreEqual("We have recieved your order. It will be processed shortly.", result0.EmailBody);

                // Check the values of item 1
                var result1 = repository.FindById(Convert.ToInt64(id1));
                Assert.IsNotNull(result1);
                Assert.AreEqual("Order Shipped", result1.EmailName);
                Assert.AreEqual("We have dispatched your order.", result1.EmailBody);

                // Attempt to remove
                repository.RemoveBy(Convert.ToInt64(id0));

                // Select count from table, it should be one
                object count = DataAccess.RunSqlCommand("SELECT COUNT(*) FROM [EmailTemplate];");
                Assert.AreEqual(1, count);

                // Select top identifier from table, it should be equal to the value in the id1 variable
                object id = Convert.ToDecimal(DataAccess.RunSqlCommand("SELECT TOP 1 [id] FROM [EmailTemplate];"));
                Assert.AreEqual(id1, id);
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }

        /// <summary>
        /// Tests the Save method of the <see cref="RyanPenfold.Repository.NHibernate.BaseRepository{T}" /> class
        /// </summary>
        [TestMethod]
        public void Save_ObjectToSave_Saved()
        {
            try
            {
                // Drop the email templates table if it exists already
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");

                // Create the email templates table
                DataAccess.RunSqlCommandFromFile("CREATE_EmailTemplate");

                // Declare expected results
                const string EmailName0 = "Your order is being processed";
                const string EmailBody0 = "Your order is being processed. Please call us if you have any questions.";
                const string EmailName1 = "Your order is processing";
                const string EmailBody1 = "Your order is processing. Please email us if you have any questions.";

                // Declare new email template
                var template = new EmailTemplate();
                {
                    // Initialise email template
                    template.EmailName = EmailName0;
                    template.EmailBody = EmailBody0;
                }

                // Create new repository
                var repository = new EmailTemplateRepository();

                // Save the template
                repository.Save(template);

                // Select count from table, it should be one
                object count = DataAccess.RunSqlCommand("SELECT COUNT(*) FROM [EmailTemplate];");
                Assert.AreEqual(1, count);

                // Assert
                Assert.AreEqual(EmailName0, DataAccess.RunSqlCommand("SELECT TOP 1 [emailName] FROM [EmailTemplate];"));
                Assert.AreEqual(EmailBody0, DataAccess.RunSqlCommand("SELECT TOP 1 [emailBody] FROM [EmailTemplate];"));

                // Amend the properties
                template.EmailName = EmailName1;
                template.EmailBody = EmailBody1;

                // Re-save the template
                repository.Save(template);

                // Re-assert
                Assert.AreEqual(EmailName1, DataAccess.RunSqlCommand("SELECT TOP 1 [emailName] FROM [EmailTemplate];"));
                Assert.AreEqual(EmailBody1, DataAccess.RunSqlCommand("SELECT TOP 1 [emailBody] FROM [EmailTemplate];"));
            }
            finally
            {
                // Drop the email templates table if it still exists
                DataAccess.RunSqlCommandFromFile("DROP_EmailTemplate");
            }
        }
    }
}
