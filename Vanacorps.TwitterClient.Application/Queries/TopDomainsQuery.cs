using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Queries
{
    public class TopDomainsQuery : ITopDomainsQuery
    {
        private readonly ITopDomainsRepository _repository;

        public TopDomainsQuery(ITopDomainsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TopDomains>> QueryAsync()
        {
            return await _repository.GetTopDomainsAsync();
        }
    }
}
