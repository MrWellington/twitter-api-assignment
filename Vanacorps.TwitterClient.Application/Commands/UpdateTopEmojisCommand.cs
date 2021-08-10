using System;
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
            
        }
    }
}
