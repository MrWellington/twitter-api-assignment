using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopEmojisRepository : ITopEmojisRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopEmojisRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopEmojis>> GetTopEmojisAsync()
        {
            return await _context.TopEmojis.OrderByDescending(e => e.Count).Take(5).ToListAsync();
        }

        public async Task UpdateTopEmojisAsync(Dictionary<string, int> newEmojis)
        {
            var updateCommands = new List<Task>();

            foreach (var newEmojiCount in newEmojis)
            {
                updateCommands.Add(UpdateEmojiCount(newEmojiCount.Key, newEmojiCount.Value));
            }

            await Task.WhenAll(updateCommands);
        }

        private async Task UpdateEmojiCount(string emoji, int count)
        {
            var emojiExists = await _context.TopEmojis.AnyAsync(e => e.Emoji == emoji);

            using var transaction = _context.Database.BeginTransaction();

            if (!emojiExists)
            {
                await _context.TopEmojis.AddAsync(new TopEmojis
                {
                    Emoji = emoji,
                    Count = count
                });
            }
            else
            {

                var existingRecord = _context.TopEmojis.Where(d => d.Emoji == emoji).Single();
                existingRecord.Count += count;
            }

            await transaction.CommitAsync();
        }
    }
}
