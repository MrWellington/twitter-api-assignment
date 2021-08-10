using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopHashtagsRepository : ITopHashtagsRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopHashtagsRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, int>> GetTopHashtags()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopHashtags(Dictionary<string, int> newHashtags)
        {
            throw new NotImplementedException();
        }
    }
}
