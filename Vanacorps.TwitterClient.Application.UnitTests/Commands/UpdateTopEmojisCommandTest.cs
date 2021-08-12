using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Vanacorps.TwitterClient.Application.Commands;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Commands
{
    public class UpdateTopEmojiCommandTest
    {
        [Fact]
        public async Task UpdateTopEmojiCommand_ProcessesNoEmojisCorrectly()
        {
            Mock<ITopEmojisRepository> mockRepo = new Mock<ITopEmojisRepository>();

            var command = new UpdateTopEmojisCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "No emojis here!"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopEmojisAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 0)));
        }

        [Fact]
        public async Task UpdateTopEmojiCommand_ProcessesSingleEmojiCorrectly()
        {
            Mock<ITopEmojisRepository> mockRepo = new Mock<ITopEmojisRepository>();

            var command = new UpdateTopEmojisCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "Pretext 😎 posttext"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopEmojisAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 1 &&
                actualArgs["😎"] == 1)));
        }

        [Fact]
        public async Task UpdateTopEmojiCommand_ProcessesMultipleEmojisCorrectly()
        {
            Mock<ITopEmojisRepository> mockRepo = new Mock<ITopEmojisRepository>();

            var command = new UpdateTopEmojisCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "😎 - 😎 - 🤔"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopEmojisAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 2 &&
                actualArgs["😎"] == 2 &&
                actualArgs["🤔"] == 1)));
        }
    }
}
