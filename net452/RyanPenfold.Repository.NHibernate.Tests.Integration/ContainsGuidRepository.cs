// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainsGuidRepository.cs" company="Ryan Penfold">
//   Copyright ? Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Repository.NHibernate.Tests.Integration
{
    /// <summary>
    /// Provides functionality relating to the management of persistent <see cref="ContainsGuid" /> objects.
    /// </summary>
    public class ContainsGuidRepository : BaseRepository<ContainsGuid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsGuidRepository"/> class. 
        /// </summary>
        public ContainsGuidRepository() : base(new BaseClassMap<ContainsGuid>())
        {            
        }
    }
}
