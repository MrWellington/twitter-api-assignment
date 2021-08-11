using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class AverageTweetQuery : IAverageTweetQuery
    {
        private readonly IProcessedTweetRepository _repository;

        public AverageTweetQuery(IProcessedTweetRepository repository)
        {
            _repository = repository;
        }

        public async Task<TweetAveragesDto> QueryAsync()
        {
            var tweets = await _repository.GetAllTweetsAsync();

            return new TweetAveragesDto
            {
                BySecond = AverageBySecond(tweets),
                ByMinute = AverageByMinute(tweets),
                ByHour = AverageByHour(tweets)
            };
        }

        private decimal AverageBySecond(List<ProcessedTweet> tweetDates)
        {
            if (tweetDates.Count() == 0)
            {
                return 0;
            }

            var binnedSeconds = tweetDates.GroupBy(x => x.ReceivedTime.ToString("dd-MM-yy HH:mm:ss")).Select(g => g.Count());

            decimal average = binnedSeconds.Sum() / binnedSeconds.Count();

            return average;
        }

        private decimal AverageByMinute(List<ProcessedTweet> tweetDates)
        {
            if (tweetDates.Count() == 0)
            {
                return 0;
            }

            var binnedMinutes = tweetDates.GroupBy(x => x.ReceivedTime.ToString("dd-MM-yy HH:mm")).Select(g => g.Count());

            decimal average = binnedMinutes.Sum() / binnedMinutes.Count();

            return average;
        }

        private decimal AverageByHour(List<ProcessedTweet> tweetDates)
        {
            if (tweetDates.Count() == 0)
            {
                return 0;
            }

            var binnedHours = tweetDates.GroupBy(x => x.ReceivedTime.ToString("dd-MM-yy HH")).Select(g => g.Count());

            decimal average = binnedHours.Sum() / binnedHours.Count();

            return average;
        }
    }
}
