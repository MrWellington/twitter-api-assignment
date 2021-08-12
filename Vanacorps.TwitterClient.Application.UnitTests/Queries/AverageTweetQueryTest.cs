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
    public class AverageTweetQueryTest
    {
        [Fact]
        public async Task AverageTweetQuery_ProcessesAverageSecondsCorrectly()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            DateTime referenceTime = DateTime.Now;

            testRepo.Setup(repo => repo.GetAllTweetsAsync()).ReturnsAsync(new List<ProcessedTweet>
            {
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-1) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-2) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddSeconds(-3) }
            });

            AverageTweetQuery testQuery = new AverageTweetQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(3, actual.BySecond);
        }

        [Fact]
        public async Task AverageTweetQuery_ProcessesAverageMinutesCorrectly()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            DateTime referenceTime = DateTime.Now;

            testRepo.Setup(repo => repo.GetAllTweetsAsync()).ReturnsAsync(new List<ProcessedTweet>
            {
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-1) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-2) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddMinutes(-3) }
            });

            AverageTweetQuery testQuery = new AverageTweetQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(3, actual.ByMinute);
        }

        [Fact]
        public async Task AverageTweetQuery_ProcessesAverageHoursCorrectly()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            DateTime referenceTime = DateTime.Now;

            testRepo.Setup(repo => repo.GetAllTweetsAsync()).ReturnsAsync(new List<ProcessedTweet>
            {
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-1) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-1) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-2) },
                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-2) },

                new ProcessedTweet { ReceivedTime = referenceTime.AddHours(-3) }
            });

            AverageTweetQuery testQuery = new AverageTweetQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(3, actual.ByHour);
        }

        [Fact]
        public async Task AverageTweetQuery_ProcessesNoTweetsCorrectly()
        {
            Mock<IProcessedTweetRepository> testRepo = new Mock<IProcessedTweetRepository>();

            DateTime referenceTime = DateTime.Now;

            testRepo.Setup(repo => repo.GetAllTweetsAsync()).ReturnsAsync(new List<ProcessedTweet>());

            AverageTweetQuery testQuery = new AverageTweetQuery(testRepo.Object);

            var actual = await testQuery.QueryAsync();

            Assert.Equal(0, actual.BySecond);
            Assert.Equal(0, actual.ByMinute);
            Assert.Equal(0, actual.ByHour);
        }
    }
}
