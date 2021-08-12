using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Vanacorps.TwitterClient.Application.Commands;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests.Commands
{
    public class UpdateTopDomainCommandTest
    {
        [Fact]
        public async Task UpdateTopDomainCommand_ProcessesNoDomainsCorrectly()
        {
            Mock<ITopDomainsRepository> mockRepo = new Mock<ITopDomainsRepository>();

            var command = new UpdateTopDomainsCommand(Mock.Of<ILogger<UpdateTopDomainsCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "No domains here!"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopDomainsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 0)));
        }

        [Fact]
        public async Task UpdateTopDomainCommand_ProcessesSingleDomainCorrectly()
        {
            Mock<ITopDomainsRepository> mockRepo = new Mock<ITopDomainsRepository>();

            var command = new UpdateTopDomainsCommand(Mock.Of<ILogger<UpdateTopDomainsCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "Pretext https://web.site posttext"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopDomainsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 1 &&
                actualArgs["web.site"] == 1)));
        }

        [Fact]
        public async Task UpdateTopDomainCommand_ProcessesMultipleDomainsCorrectly()
        {
            Mock<ITopDomainsRepository> mockRepo = new Mock<ITopDomainsRepository>();

            var command = new UpdateTopDomainsCommand(Mock.Of<ILogger<UpdateTopDomainsCommand>>(), mockRepo.Object);

            await command.ExecuteAsync(new Tweet
            {
                data = new TweetData
                {
                    id = "1",
                    text = "https://reuters.com/news - http://reuters.com/news - https://t.co/route"
                }
            });

            mockRepo.Verify(repo => repo.UpdateTopDomainsAsync(It.Is<Dictionary<string, int>>(actualArgs =>
                actualArgs.Count == 2 &&
                actualArgs["reuters.com"] == 2 &&
                actualArgs["t.co"] == 1)));
        }
    }
}
