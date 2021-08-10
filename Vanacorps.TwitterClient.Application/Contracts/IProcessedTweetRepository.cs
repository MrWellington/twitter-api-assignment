using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface IProcessedTweetRepository
    {
        Task AddTweetAsync(ProcessedTweet tweet);
        Task<int> GetTweetCountAsync();
        Task<float> GetEmojiPercent();
        Task<float> GetUrlPercent();
        Task<float> GetPhotoUrlPercent();
    }

}
