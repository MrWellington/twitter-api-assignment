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
    public class TopEmojisQueryTest
    {
        [Fact]
        public async Task TopEmojisQuery_ReturnsTopEmojisReceivedFromRepository()
        {
            Mock<ITopEmojisRepository> testRepo = new Mock<ITopEmojisRepository>();

            var testEmojis = new List<TopEmojis>
            {
                new TopEmojis
                {
                    Emoji = "😂",
                    Count = 2
                }
            };

            testRepo.Setup(repo => repo.GetTopEmojisAsync()).ReturnsAsync(testEmojis);

            TopEmojisQuery testQuery = new TopEmojisQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testEmojis, actual);
        }

        [Fact]
        public async Task TopHashtagsQuery_ReturnsEmptyListForNoTweets()
        {
            Mock<ITopEmojisRepository> testRepo = new Mock<ITopEmojisRepository>();

            var testEmojis = new List<TopEmojis>();

            testRepo.Setup(repo => repo.GetTopEmojisAsync()).ReturnsAsync(testEmojis);

            TopEmojisQuery testQuery = new TopEmojisQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testEmojis, actual);
        }
    }
}
