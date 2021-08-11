using System.Linq;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class EmojiPercentQuery : IEmojiPercentQuery
    {
        private readonly IProcessedTweetRepository _repository;

        public EmojiPercentQuery(IProcessedTweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> QueryAsync()
        {
            var emojiStatuses = await _repository.GetEmojiStatusAsync();

            if (emojiStatuses.Count == 0)
            {
                return 0;
            }

            int tweetsWithPhotos = emojiStatuses.Select(p => p).Count();

            decimal percent = tweetsWithPhotos / emojiStatuses.Count();
            percent *= 100;

            return percent;
        }
    }
}
