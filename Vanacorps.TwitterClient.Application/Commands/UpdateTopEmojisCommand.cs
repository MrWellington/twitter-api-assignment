using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Commands
{
    public class UpdateTopEmojisCommand : IUpdateTopEmojisCommand
    {
        private readonly ILogger<UpdateTopEmojisCommand> _logger;
        private readonly ITopEmojisRepository _repository;
        public UpdateTopEmojisCommand(ILogger<UpdateTopEmojisCommand> logger, ITopEmojisRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task ExecuteAsync(Tweet tweet)
        {
            _logger.LogDebug("Execute UpdateTopEmojisCommand");

            var newEmojiCounts = GetNewEmojiCounts(tweet.data.text);

            await _repository.UpdateTopEmojisAsync(newEmojiCounts);
        }

        private Dictionary<string, int> GetNewEmojiCounts(string message)
        {
            var newEmojiCounts = new Dictionary<string, int>();
            var emojiMatches = RegexHelper.Emoji.Matches(message);

            foreach (Match emojiMatch in emojiMatches)
            {
                var emoji = emojiMatch.Value;

                if (!newEmojiCounts.ContainsKey(emoji))
                {
                    newEmojiCounts.Add(emoji, 1);
                }
                else
                {
                    newEmojiCounts[emoji] += 1;
                }
            }

            return newEmojiCounts;
        }
    }
}
