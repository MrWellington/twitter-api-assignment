using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Application.Queries;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Queries
{
    public class EmojiPercentQueryTest
    {
        [Fact]
        public async Task EmojiPercentQuery_ReturnsCorrectWholePercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetEmojiStatusAsync()).ReturnsAsync(new List<bool> { true, false });

            EmojiPercentQuery testQuery = new EmojiPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(50.00M, actual);
        }

        [Fact]
        public async Task EmojiPercentQuery_ReturnsCorrectFractionalPercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetEmojiStatusAsync()).ReturnsAsync(new List<bool> { true, true, false });

            EmojiPercentQuery testQuery = new EmojiPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(66.67M, actual);
        }

        [Fact]
        public async Task EmojiPercentQuery_ReturnsZeroForNoTweets()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetEmojiStatusAsync()).ReturnsAsync(new List<bool>());

            EmojiPercentQuery testQuery = new EmojiPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(0M, actual);
        }
    }
}
