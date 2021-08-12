using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application.Commands;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.TweetProcessor
{
    public class Processor : IConsumer<Tweet>
    {
        private readonly ILogger<Processor> _logger;
        private readonly IProcessTweetCommand _tweetCommand;
        private readonly IUpdateTopDomainsCommand _domainsCommand;
        private readonly IUpdateTopEmojisCommand _emojisCommand;
        private readonly IUpdateTopHashtagsCommand _hashtagsCommand;

        // Class to processes tweets from a messaging service
        public Processor(ILogger<Processor> logger,
            IProcessTweetCommand tweetCommand,
            IUpdateTopDomainsCommand domainsCommand,
            IUpdateTopEmojisCommand emojisCommand,
            IUpdateTopHashtagsCommand hashtagsCommand)
        {
            _logger = logger;
            _tweetCommand = tweetCommand;
            _domainsCommand = domainsCommand;
            _emojisCommand = emojisCommand;
            _hashtagsCommand = hashtagsCommand;
        }

        // MassTransit message consumer that pops and processes tweets off queue
        public async Task Consume(ConsumeContext<Tweet> context)
        {
            _logger.LogDebug($"Tweet id {context.Message.data.id} received for processing.");

            var newTweet = context.Message;

            try
            {
                var processingTasks = new List<Task>
                {
                    _tweetCommand.ExecuteAsync(newTweet),
                    _domainsCommand.ExecuteAsync(newTweet),
                    _emojisCommand.ExecuteAsync(newTweet),
                    _hashtagsCommand.ExecuteAsync(newTweet)
                };

                await Task.WhenAll(processingTasks);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    _logger.LogError(e, "Error occurred processing tweet: ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred processing tweet: ");
            }

        }
    }
}
