using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ITopHashtagsRepository
    {
        Task UpdateTopHashtags(Dictionary<string, int> newHashtags);
        Task<Dictionary<string, int>> GetTopHashtags();
    }
}
