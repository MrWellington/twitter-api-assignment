using System.Threading.Tasks;
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

        public Task<float> GetEmojiPercent()
        {
            throw new System.NotImplementedException();
        }

        public Task<float> GetPhotoUrlPercent()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetTweetCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<float> GetUrlPercent()
        {
            throw new System.NotImplementedException();
        }
    }
}
