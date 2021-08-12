using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Application.Queries;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Queries
{
    public class UrlPercentQueryTest
    {
        [Fact]
        public async Task UrlPercentQuery_ReturnsCorrectWholePercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetUrlStatusAsync()).ReturnsAsync(new List<bool> { true, false });

            UrlPercentQuery testQuery = new UrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(50.00M, actual);
        }

        [Fact]
        public async Task UrlPercentQuery_ReturnsCorrectFractionalPercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetUrlStatusAsync()).ReturnsAsync(new List<bool> { true, true, false });

            UrlPercentQuery testQuery = new UrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(66.67M, actual);
        }

        [Fact]
        public async Task UrlPercentQuery_ReturnsZeroForNoTweets()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetUrlStatusAsync()).ReturnsAsync(new List<bool>());

            UrlPercentQuery testQuery = new UrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(0M, actual);
        }
    }
}
