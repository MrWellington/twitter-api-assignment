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
        private readonly ICommand<Tweet> _tweetCommand;

        public Processor(ILogger<Processor> logger, ICommand<Tweet> tweetCommand)
        {
            _logger = logger;
            _tweetCommand = tweetCommand;
        }

        public async Task Consume(ConsumeContext<Tweet> context)
        {
            _logger.LogDebug($"Tweet id {context.Message.data.id} received for processing.");

            await _tweetCommand.ExecuteAsync(context.Message);
        }
    }
}
