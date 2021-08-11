using System;
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

            if (urlStatuses.Count == 0)
            {
                return 0;
            }

            decimal tweetsWithUrl = urlStatuses.Count(t => t);

            decimal percent = tweetsWithUrl / (decimal)urlStatuses.Count();
            percent *= 100;

            return decimal.Round(percent, 2, MidpointRounding.AwayFromZero);
        }
    }
}
