using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class ProcessedTweetRepository : IProcessedTweetRepository
    {
        private readonly TwitterClientDbContext _context;

        public ProcessedTweetRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public async Task AddTweetAsync(ProcessedTweet tweet)
        {
            await _context.ProcessedTweets.AddAsync(tweet);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTweetCountAsync()
        {
            return await _context.ProcessedTweets.CountAsync();
        }

        public async Task<IList<DateTime>> GetAllDateTimesAsync()
        {
            return await _context.ProcessedTweets.Select(u => u.ReceivedTime).ToListAsync();
        }

        public async Task<IList<bool>> GetEmojiStatusAsync()
        {
            return await _context.ProcessedTweets.Select(u => u.ContainsEmojis).ToListAsync();
        }

        public async Task<IList<bool>> GetPhotoUrlStatusAsync()
        {
            return await _context.ProcessedTweets.Select(u => u.ContainsPhotoUrl).ToListAsync();
        }

        public async Task<IList<bool>> GetUrlStatusAsync()
        {
            return await _context.ProcessedTweets.Select(u => u.ContainsUrl).ToListAsync();
        }
    }
}
