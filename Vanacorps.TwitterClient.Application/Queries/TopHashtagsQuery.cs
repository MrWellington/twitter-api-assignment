using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class TopHashtagsQuery : ITopHashtagsQuery
    {
        private readonly ITopHashtagsRepository _repository;

        public TopHashtagsQuery(ITopHashtagsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TopHashtags>> QueryAsync()
        {
            return await _repository.GetTopHashtagsAsync();
        }
    }
}
