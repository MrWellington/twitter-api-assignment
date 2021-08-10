using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ITopEmojisRepository
    {
        Task UpdateTopEmojisAsync(Dictionary<string, int> newEmojis);
        Task<List<TopEmojis>> GetTopEmojisAsync();
    }
}
