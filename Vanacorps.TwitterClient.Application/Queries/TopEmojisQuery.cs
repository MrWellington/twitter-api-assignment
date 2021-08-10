using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class TopEmojisQuery : ITopEmojisQuery
    {
        private readonly ITopEmojisRepository _repository;

        public TopEmojisQuery(ITopEmojisRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TopEmojis>> QueryAsync()
        {
            return await _repository.GetTopEmojisAsync();
        }
    }
}
