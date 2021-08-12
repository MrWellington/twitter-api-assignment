using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Application.Queries;
using Vanacorps.TwitterClient.Domain;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Queries
{
    public class TopDomainsQueryTest
    {
        [Fact]
        public async Task TopDomainsQuery_ReturnsTopDomainsReceivedFromRepository()
        {
            Mock<ITopDomainsRepository> testRepo = new Mock<ITopDomainsRepository>();

            var testDomains = new List<TopDomains>
            {
                new TopDomains
                {
                    Domain = "t.co",
                    Count = 2
                }
            };

            testRepo.Setup(repo => repo.GetTopDomainsAsync()).ReturnsAsync(testDomains);

            TopDomainsQuery testQuery = new TopDomainsQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testDomains, actual);
        }

        [Fact]
        public async Task TopHashtagsQuery_ReturnsEmptyListForNoTweets()
        {
            Mock<ITopDomainsRepository> testRepo = new Mock<ITopDomainsRepository>();

            var testDomains = new List<TopDomains>();

            testRepo.Setup(repo => repo.GetTopDomainsAsync()).ReturnsAsync(testDomains);

            TopDomainsQuery testQuery = new TopDomainsQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testDomains, actual);
        }
    }
}
