using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopHashtagsRepository : ITopHashtagsRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopHashtagsRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopHashtags>> GetTopHashtagsAsync()
        {
            return await _context.TopHashtags.OrderByDescending(h => h.Count).Take(5).ToListAsync();
        }

        public async Task UpdateTopHashtagsAsync(Dictionary<string, int> newHashtags)
        {
            var updateCommands = new List<Task>();

            foreach (var newHashtagCount in newHashtags)
            {
                updateCommands.Add(UpdateHashtagCount(newHashtagCount.Key, newHashtagCount.Value));
            }

            await Task.WhenAll(updateCommands);
        }

        private async Task UpdateHashtagCount(string hashtag, int count)
        {
            var hashtagExists = await _context.TopHashtags.AnyAsync(h => h.Hashtag == hashtag);

            if (!hashtagExists)
            {
                await _context.TopHashtags.AddAsync(new TopHashtags
                {
                    Hashtag = hashtag,
                    Count = count
                });
            }
            else
            {

                var existingRecord = _context.TopHashtags.Where(d => d.Hashtag == hashtag).Single();
                existingRecord.Count += count;
            }

            await _context.SaveChangesAsync();
        }
    }
}
