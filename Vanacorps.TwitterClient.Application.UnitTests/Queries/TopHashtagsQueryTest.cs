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
    public class TopHashtagsQueryTest
    {
        [Fact]
        public async Task TopHashtagsQuery_ReturnsTopHashtagsReceivedFromRepository()
        {
            Mock<ITopHashtagsRepository> testRepo = new Mock<ITopHashtagsRepository>();

            var testHashtags = new List<TopHashtags>
            {
                new TopHashtags
                {
                    Hashtag = "#test",
                    Count = 2
                }
            };

            testRepo.Setup(repo => repo.GetTopHashtagsAsync()).ReturnsAsync(testHashtags);

            TopHashtagsQuery testQuery = new TopHashtagsQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testHashtags, actual);
        }

        [Fact]
        public async Task TopHashtagsQuery_ReturnsEmptyListForNoTweets()
        {
            Mock<ITopHashtagsRepository> testRepo = new Mock<ITopHashtagsRepository>();

            var testHashtags = new List<TopHashtags>();

            testRepo.Setup(repo => repo.GetTopHashtagsAsync()).ReturnsAsync(testHashtags);

            TopHashtagsQuery testQuery = new TopHashtagsQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Same(testHashtags, actual);
        }
    }
}
