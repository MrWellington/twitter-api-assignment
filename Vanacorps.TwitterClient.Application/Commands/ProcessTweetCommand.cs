using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Commands
{
    public class ProcessTweetCommand : IProcessTweetCommand
    {
        private readonly ILogger<ProcessTweetCommand> _logger;
        private readonly IProcessedTweetRepository _repository;
        public ProcessTweetCommand(ILogger<ProcessTweetCommand> logger, IProcessedTweetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task ExecuteAsync(Tweet tweet)
        {
            _logger.LogDebug("Execute ProcessTweetCommand");

            var pt = new ProcessedTweet
            {
                ID = tweet.data.id,
                ReceivedTime = DateTime.UtcNow,
                ContainsEmojis = HasEmojis(tweet.data.text),
                ContainsUrl = HasUrl(tweet.data.text),
                ContainsPhotoUrl = HasPhotoUrl(tweet.data.text)
            };

            await _repository.AddTweetAsync(pt);
        }

        private bool HasEmojis(string message) => RegexHelper.Emoji.IsMatch(message);

        private bool HasUrl(string message) => RegexHelper.Url.IsMatch(message);

        private bool HasPhotoUrl(string message)
        {
            var urls = RegexHelper.Url.Matches(message);

            foreach (Match url in urls)
            {
                var parsedUri = new Uri(url.Value);

                if (parsedUri.Host.Contains("pic.twitter.com") || url.Value.Contains("instagram.com/p/"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
