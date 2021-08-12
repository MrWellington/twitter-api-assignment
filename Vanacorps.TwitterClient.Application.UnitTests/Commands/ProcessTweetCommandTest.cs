using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Vanacorps.TwitterClient.Application.Commands;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Commands
{
    public class ProcessTweetCommandTest
    {
        [Fact]
        public async Task ProcessTweetCommand_ProcessesEmojisCorrectly()
        {
            Mock<IProcessedTweetRepository> mockRepo = new Mock<IProcessedTweetRepository>();

            var command = new ProcessTweetCommand(Mock.Of<ILogger<ProcessTweetCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "Tweet 🐦"
                }
            });

            mockRepo.Verify(repo => repo.AddTweetAsync(It.Is<ProcessedTweet>(actualArgs =>
                actualArgs.ContainsEmojis &&
                !actualArgs.ContainsUrl &&
                !actualArgs.ContainsPhotoUrl)));
        }

        [Fact]
        public async Task ProcessTweetCommand_ProcessesUrlsCorrectly()
        {
            Mock<IProcessedTweetRepository> mockRepo = new Mock<IProcessedTweetRepository>();

            var command = new ProcessTweetCommand(Mock.Of<ILogger<ProcessTweetCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "http://www.twitter.com/"
                }
            });

            mockRepo.Verify(repo => repo.AddTweetAsync(It.Is<ProcessedTweet>(actualArgs =>
                !actualArgs.ContainsEmojis &&
                actualArgs.ContainsUrl &&
                !actualArgs.ContainsPhotoUrl)));
        }

        [Fact]
        public async Task ProcessTweetCommand_ProcessesPhotoUrlsCorrectly()
        {
            Mock<IProcessedTweetRepository> mockRepo = new Mock<IProcessedTweetRepository>();

            var command = new ProcessTweetCommand(Mock.Of<ILogger<ProcessTweetCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "Pic: https://pic.twitter.com/route"
                }
            });

            mockRepo.Verify(repo => repo.AddTweetAsync(It.Is<ProcessedTweet>(actualArgs =>
                !actualArgs.ContainsEmojis &&
                actualArgs.ContainsUrl &&
                actualArgs.ContainsPhotoUrl)));
        }
    }
}
