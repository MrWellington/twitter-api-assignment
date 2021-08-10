using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence.Repositories
{
    public class TopDomainsRepository : ITopDomainsRepository
    {
        private readonly TwitterClientDbContext _context;

        public TopDomainsRepository(TwitterClientDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopDomains>> GetTopDomainsAsync()
        {
            return await _context.TopDomains.OrderByDescending(h => h.Count).Take(5).ToListAsync();
        }

        public async Task UpdateTopDomainsAsync(Dictionary<string, int> newDomains)
        {
            var updateCommands = new List<Task>();

            foreach (var newDomainCount in newDomains)
            {
                updateCommands.Add(UpdateDomainCount(newDomainCount.Key, newDomainCount.Value));
            }

            await Task.WhenAll(updateCommands);
        }

        private async Task UpdateDomainCount(string domain, int count)
        {
            var domainExists = await _context.TopDomains.AnyAsync(d => d.Domain == domain);

            using var transaction = _context.Database.BeginTransaction();

            if (!domainExists)
            {
                await _context.TopDomains.AddAsync(new TopDomains
                {
                    Domain = domain,
                    Count = count
                });
            }
            else {

                var existingRecord = _context.TopDomains.Where(d => d.Domain == domain).Single();
                existingRecord.Count += count;
            }

            await transaction.CommitAsync();
        }
    }
}
