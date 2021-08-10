using System;
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
            
        }
    }
}
