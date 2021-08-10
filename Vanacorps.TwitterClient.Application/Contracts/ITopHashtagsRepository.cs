using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ITopHashtagsRepository
    {
        Task UpdateTopHashtagsAsync(Dictionary<string, int> newHashtags);
        Task<List<TopHashtags>> GetTopHashtagsAsync();
    }
}
