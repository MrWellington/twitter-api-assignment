using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopEmojisRepository : ITopEmojisRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopEmojisRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, int>> GetTopEmojis()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopEmojis(Dictionary<string, int> newEmojis)
        {
            throw new NotImplementedException();
        }
    }
}
