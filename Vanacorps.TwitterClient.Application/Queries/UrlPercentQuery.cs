using System.Linq;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class UrlPercentQuery : IUrlPercentQuery
    {
        private readonly IProcessedTweetRepository _repository;

        public UrlPercentQuery(IProcessedTweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> QueryAsync()
        {
            var urlStatuses = await _repository.GetUrlStatusAsync();

            int tweetsWithPhotos = urlStatuses.Select(p => p).Count();

            decimal percent = tweetsWithPhotos / urlStatuses.Count();
            percent *= 100;

            return percent;
        }
    }
}
