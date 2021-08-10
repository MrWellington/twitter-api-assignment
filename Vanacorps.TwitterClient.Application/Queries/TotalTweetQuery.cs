using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class TotalTweetQuery : ITotalTweetQuery
    {
        private readonly IProcessedTweetRepository _repository;

        public TotalTweetQuery(IProcessedTweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> QueryAsync()
        {
            return await _repository.GetTweetCountAsync();
        }
    }
}
