using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopDomainsRepository : ITopDomainsRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopDomainsRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, int>> GetTopDomains()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopDomains(Dictionary<string, int> newDomains)
        {
            throw new NotImplementedException();
        }
    }
}
