using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Commands
{
    public class UpdateTopDomainsCommand : IUpdateTopDomainsCommand
    {
        private readonly ILogger<UpdateTopDomainsCommand> _logger;
        private readonly ITopDomainsRepository _repository;
        public UpdateTopDomainsCommand(ILogger<UpdateTopDomainsCommand> logger, ITopDomainsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task ExecuteAsync(Tweet tweet)
        {
            
        }
    }
}
