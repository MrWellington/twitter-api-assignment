using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Commands
{
    public class UpdateTopHashtagsCommand : IUpdateTopHashtagsCommand
    {
        private readonly ILogger<UpdateTopHashtagsCommand> _logger;
        private readonly ITopHashtagsRepository _repository;
        public UpdateTopHashtagsCommand(ILogger<UpdateTopHashtagsCommand> logger, ITopHashtagsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task ExecuteAsync(Tweet tweet)
        {
            var newHashtagCounts = GetNewHashtagCounts(tweet.data.text);

            await _repository.UpdateTopHashtagsAsync(newHashtagCounts);
        }

        private Dictionary<string, int> GetNewHashtagCounts(string message)
        {
            var newHashtagCounts = new Dictionary<string, int>();
            var hashtagMatches = RegexHelper.Hashtag.Matches(message);

            foreach (Match hashtagMatch in hashtagMatches)
            {
                var hashtag = hashtagMatch.Value;

                if (!newHashtagCounts.ContainsKey(hashtag))
                {
                    newHashtagCounts.Add(hashtag, 1);
                }
                else
                {
                    newHashtagCounts[hashtag] += 1;
                }
            }

            return newHashtagCounts;
        }
    }
}
