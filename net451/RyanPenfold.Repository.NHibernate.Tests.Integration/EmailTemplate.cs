// --------------------------------------------------------------------------------------------------------------------
// <copyright file="emailTemplate.cs" company="Ryan Penfold">
//   Copyright ? Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   16 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    using RyanPenfold.BusinessBase.Infrastructure;

    /// <summary>
    /// An email template
    /// </summary>
    public class EmailTemplate : IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id
        {
            get
            {
                return this.EmailTemplateId;
            }

            set
            {
                this.EmailTemplateId = value;
            }
        }

        /// <summary>
        /// Gets or sets the email template body
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// Gets or sets the name or title of the email template
        /// </summary>
        public string EmailName { get; set; }

        /// <summary>
        /// Gets or sets the identity of the email template
        /// </summary>
        public long EmailTemplateId { get; set; }
    }
}
