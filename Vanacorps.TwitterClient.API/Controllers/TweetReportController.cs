using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetReportController : ControllerBase
    {
        private readonly ILogger<TweetReportController> _logger;
        private readonly IAverageTweetQuery _averageTweetQuery;
        private readonly IEmojiPercentQuery _emojiPercentQuery;
        private readonly IPhotoUrlPercentQuery _photoUrlPercentQuery;
        private readonly ITopDomainsQuery _topDomainsQuery;
        private readonly ITopEmojisQuery _topEmojisQuery;
        private readonly ITopHashtagsQuery _topHashtagsQuery;
        private readonly ITotalTweetQuery _totalTweetQuery;
        private readonly IUrlPercentQuery _urlPercentQuery;

        public TweetReportController(ILogger<TweetReportController> logger,
            IAverageTweetQuery averageTweetQuery,
            IEmojiPercentQuery emojiPercentQuery,
            IPhotoUrlPercentQuery photoUrlPercentQuery,
            ITopDomainsQuery topDomainsQuery,
            ITopEmojisQuery topEmojisQuery,
            ITopHashtagsQuery topHashtagsQuery,
            ITotalTweetQuery totalTweetQuery,
            IUrlPercentQuery urlPercentQuery)
        {
            _logger = logger;
            _averageTweetQuery = averageTweetQuery;
            _emojiPercentQuery = emojiPercentQuery;
            _photoUrlPercentQuery = photoUrlPercentQuery;
            _topDomainsQuery = topDomainsQuery;
            _topEmojisQuery = topEmojisQuery;
            _topHashtagsQuery = topHashtagsQuery;
            _totalTweetQuery = totalTweetQuery;
            _urlPercentQuery = urlPercentQuery;
        }

        [HttpGet]
        public async Task<TweetReportDto> Get()
        {
            var reportQueries = new List<Task>
            {
                _averageTweetQuery.QueryAsync(),
                _emojiPercentQuery.QueryAsync(),
                _photoUrlPercentQuery.QueryAsync(),
                _topDomainsQuery.QueryAsync(),
                _topEmojisQuery.QueryAsync(),
                _topHashtagsQuery.QueryAsync(),
                _totalTweetQuery.QueryAsync(),
                _urlPercentQuery.QueryAsync()
            };

            await Task.WhenAll(reportQueries);

            return new TweetReportDto
            {
                TweetAverages = ((Task<TweetAveragesDto>)reportQueries[0]).Result,
                PercentWithEmojis = ((Task<decimal>)reportQueries[1]).Result,
                PercentWithPhotoUrls = ((Task<decimal>)reportQueries[2]).Result,
                TopDomains = ((Task<List<TopDomains>>)reportQueries[3]).Result,
                TopEmojis = ((Task<List<TopEmojis>>)reportQueries[4]).Result,
                TopHashtags = ((Task<List<TopHashtags>>)reportQueries[5]).Result,
                TotalTweetCount = ((Task<int>)reportQueries[6]).Result,
                PercentWithUrls = ((Task<decimal>)reportQueries[7]).Result,
            };
        }
    }
}
