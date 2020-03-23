// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="Ryan Penfold">
//   Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   17 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Unit
{
    /// <summary>
    /// A test class to map to a test table
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Gets or sets the test class id.
        /// </summary>
        public string TestClassId { get; set; }

        /// <summary>
        /// Gets or sets the test class name.
        /// </summary>
        public string TestClassName { get; set; }
    }
}
