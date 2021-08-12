using System;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Application.Queries;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Queries
{
    public class TotalTweetQueryTest
    {
        [Fact]
        public async Task TotalTweetQuery_ReturnsZeroWithNoTweets()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetTweetCountAsync()).ReturnsAsync(0);

            TotalTweetQuery testQuery = new TotalTweetQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(0, actual);
        }
    }
}
