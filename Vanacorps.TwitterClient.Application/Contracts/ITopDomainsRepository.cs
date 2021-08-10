using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application.Contracts
{
    public interface ITopDomainsRepository
    {
        Task UpdateTopDomainsAsync(Dictionary<string, int> newDomains);
        Task<List<TopDomains>> GetTopDomainsAsync();
    }
}
