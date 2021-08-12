using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Application.Queries;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Queries
{
    public class PhotoUrlPercentQueryTest
    {
        [Fact]
        public async Task PhotoUrlPercentQuery_ReturnsCorrectWholePercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetPhotoUrlStatusAsync()).ReturnsAsync(new List<bool> { true, false });

            PhotoUrlPercentQuery testQuery = new PhotoUrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(50.00M, actual);
        }

        [Fact]
        public async Task PhotoUrlPercentQuery_ReturnsCorrectFractionalPercentage()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetPhotoUrlStatusAsync()).ReturnsAsync(new List<bool> { true, true, false });

            PhotoUrlPercentQuery testQuery = new PhotoUrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(66.67M, actual);
        }

        [Fact]
        public async Task PhotoUrlPercentQuery_ReturnsZeroForNoTweets()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            testRepo.Setup(repo => repo.GetPhotoUrlStatusAsync()).ReturnsAsync(new List<bool>());

            PhotoUrlPercentQuery testQuery = new PhotoUrlPercentQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(0M, actual);
        }
    }
}
