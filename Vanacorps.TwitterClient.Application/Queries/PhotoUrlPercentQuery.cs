using System;
using System.Linq;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class PhotoUrlPercentQuery : IPhotoUrlPercentQuery
    {
        private readonly IProcessedTweetRepository _repository;

        public PhotoUrlPercentQuery(IProcessedTweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> QueryAsync()
        {
            var photoUrlStatuses = await _repository.GetPhotoUrlStatusAsync();

            if (photoUrlStatuses.Count == 0)
            {
                return 0;
            }

            decimal tweetsWithPhotos = photoUrlStatuses.Count(t => t);

            decimal percent = tweetsWithPhotos / (decimal)photoUrlStatuses.Count();
            percent *= 100;

            return decimal.Round(percent, 2, MidpointRounding.AwayFromZero);
        }
    }
}
