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
    public class UpdateTopHashtagCommandTest
    {
        [Fact]
        public async Task UpdateTopHashtagCommand_ProcessesNoHashtagsCorrectly()
        {
            Mock<ITopHashtagsRepository> mockRepo = new Mock<ITopHashtagsRepository>();

            var command = new UpdateTopHashtagsCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "No hashtags here!"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopHashtagsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 0)));
        }

        [Fact]
        public async Task UpdateTopHashtagCommand_ProcessesSingleHashtagCorrectly()
        {
            Mock<ITopHashtagsRepository> mockRepo = new Mock<ITopHashtagsRepository>();

            var command = new UpdateTopHashtagsCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "Pretext #test posttext"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopHashtagsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 1 &&
                actualArgs["#test"] == 1)));
        }

        [Fact]
        public async Task UpdateTopHashtagCommand_ProcessesMultipleHashtagsCorrectly()
        {
            Mock<ITopHashtagsRepository> mockRepo = new Mock<ITopHashtagsRepository>();

            var command = new UpdateTopHashtagsCommand(null, mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "#two #one #two"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopHashtagsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 2 &&
                actualArgs["#two"] == 2 &&
                actualArgs["#one"] == 1)));
        }
    }
}
