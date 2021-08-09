using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.TweetProcessor
{
    public class Processor : IConsumer<Tweet>
    {
        private readonly ILogger<Processor> _logger;

        public Processor(ILogger<Processor> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Tweet> context)
        {
            return Task.CompletedTask;
        }
    }
}
