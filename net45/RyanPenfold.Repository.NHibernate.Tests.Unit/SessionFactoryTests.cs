// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionFactoryTests.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   17 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RyanPenfold.Repository.NHibernate.Tests.Unit
{
    using Configuration;

    using NHibernate;

    /// <summary>
    /// Provides unit tests for the <see cref="SessionFactory" /> class
    /// </summary>
    [TestClass]
    public class SessionFactoryTests
    {
        /// <summary>
        /// Tests the GetNewSession of the <see cref="SessionFactory"/> class.
        /// </summary>
        [TestMethod]
        public void GetNewSession_SetMockFactory_OpenSessionMethodCalled()
        {
            // Arrange
            var mockSession = new Mock<global::NHibernate.ISession>();
            var mockFactory = new Mock<global::NHibernate.ISessionFactory>();
            mockFactory.Setup(x => x.OpenSession()).Returns(mockSession.Object);
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.BuildSessionFactory()).Returns(mockFactory.Object);
            var sessionFactory = new SessionFactory(mockConfiguration.Object);

            // Act
            var result = sessionFactory.GetNewSession();

            // Assert
            Assert.AreEqual(mockSession.Object, result);
            mockFactory.Verify(x => x.OpenSession());
        }

        /// <summary>
        /// Tests the constructor of the <see cref="RyanPenfold.Repository.NHibernate.SessionFactory"/> class.
        /// </summary>
        [TestMethod]
        public void New_MockConfigurationClass_ConfiguredAndInstantiated()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();

            // Act
            var sessionFactory = new SessionFactory(mockConfiguration.Object);

            // Assert
            Assert.IsNotNull(sessionFactory);
            mockConfiguration.Verify(x => x.Configure());
            mockConfiguration.Verify(x => x.BuildSessionFactory());
        }
    }
}