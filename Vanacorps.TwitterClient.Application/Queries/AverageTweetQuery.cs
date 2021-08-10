using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;

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
            var tweetDates = await _repository.GetAllDateTimesAsync();

            return new TweetAveragesDto
            {
                BySecond = AverageBySecond(tweetDates),
                ByMinute = AverageByMinute(tweetDates),
                ByHour = AverageByHour(tweetDates)
            };
        }

        private decimal AverageBySecond(IEnumerable<DateTime> tweetDates)
        {
            IEnumerable<int> binnedSeconds = tweetDates.GroupBy(date => date.Second).Select(grp => grp.Count());

            decimal average = binnedSeconds.Sum() / binnedSeconds.Count();

            return average;
        }

        private decimal AverageByMinute(IEnumerable<DateTime> tweetDates)
        {
            IEnumerable<int> binnedMinutes = tweetDates.GroupBy(date => date.Minute).Select(grp => grp.Count());

            decimal average = binnedMinutes.Sum() / binnedMinutes.Count();

            return average;
        }

        private decimal AverageByHour(IEnumerable<DateTime> tweetDates)
        {
            IEnumerable<int> binnedHours = tweetDates.GroupBy(date => date.Hour).Select(grp => grp.Count());

            decimal average = binnedHours.Sum() / binnedHours.Count();

            return average;
        }
    }
}
