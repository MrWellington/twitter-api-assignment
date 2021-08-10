using System;
using System.Collections.Generic;
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
            var newDomainCounts = GetNewDomainCounts(tweet.data.text);

            await _repository.UpdateTopDomains(newDomainCounts);
        }

        private Dictionary<string, int> GetNewDomainCounts(string message)
        {
            var newDomainCounts = new Dictionary<string, int>();
            var urlMatches = RegexHelper.Url.Matches(message);

            foreach (Match url in urlMatches)
            {
                var parsedUri = new Uri(url.Value);
                var domain = parsedUri.Host;

                if (!newDomainCounts.ContainsKey(domain))
                {
                    newDomainCounts.Add(domain, 1);
                }
                else 
                {
                    newDomainCounts[domain] += 1;
                }
            }

            return newDomainCounts;
        }
    }
}
