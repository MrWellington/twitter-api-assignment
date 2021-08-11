using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface IProcessedTweetRepository
    {
        Task AddTweetAsync(ProcessedTweet tweet);
        Task<int> GetTweetCountAsync();
        Task<IList<bool>> GetEmojiStatusAsync();
        Task<IList<bool>> GetUrlStatusAsync();
        Task<IList<bool>> GetPhotoUrlStatusAsync();
        Task<List<ProcessedTweet>> GetAllTweetsAsync();
    }

}
