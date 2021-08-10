using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Commands
{
    public class ProcessTweetCommand
    {
        private readonly ILogger<ProcessTweetCommand> _logger;
        public ProcessTweetCommand(ILogger<ProcessTweetCommand> logger)
        {
            _logger = logger;
        }

        public static void Execute(Tweet tweet)
        {
            var pt = new ProcessedTweet
            {
                ID = tweet.data.id,
                ReceivedTime = DateTime.UtcNow,
                ContainsEmojis = HasEmojis(tweet.data.text),
                ContainsUrl = HasUrl(tweet.data.text),
                ContainsPhotoUrl = HasPhotoUrl(tweet.data.text)
            };

            
        }

        private static bool HasEmojis(string message) => RegexHelper.Emoji.IsMatch(message);

        private static bool HasUrl(string message) => RegexHelper.Url.IsMatch(message);

        private static bool HasPhotoUrl(string message)
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
