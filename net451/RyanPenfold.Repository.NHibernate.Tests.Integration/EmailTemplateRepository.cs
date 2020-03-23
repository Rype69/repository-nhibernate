// --------------------------------------------------------------------------------------------------------------------
// <copyright file="emailTemplateRepository.cs" company="Ryan Penfold">
//   Copyright ? Ryan Penfold. All rights reserved.
// </copyright>
// <date>
//   16 November 2011
// </date>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    /// <summary>
    /// Provides functionality relating to the management of persistent <see cref="EmailTemplate" /> objects.
    /// </summary>
    public class EmailTemplateRepository : BaseRepository<EmailTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateRepository"/> class. 
        /// </summary>
        public EmailTemplateRepository() : base(new BaseClassMap<EmailTemplate>())
        {            
        }
    }
}
