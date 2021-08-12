using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Vanacorps.TwitterClient.Application.UnitTests
{
    public class RegexHelperTest
    {
        [Theory]
        [InlineData("no emoji", 0)]
        [InlineData("no emoji :)", 0)]
        [InlineData("❤️", 1)]
        [InlineData("pretext 😭", 1)]
        [InlineData("😂 posttext", 1)]
        [InlineData("pretext 👍 posttext", 1)]
        [InlineData("✨ emojitext ✨", 2)]
        [InlineData("pretext ✨ emojitext ✨ posttext", 2)]
        public void EmojiRegex_ProperlyMatchesEmojis(string tweetText, int expectedResult)
        {
            int matchCount = RegexHelper.Emoji.Matches(tweetText).Count;
            Assert.Equal(expectedResult, matchCount);
        }

        [Theory]
        [InlineData("http://", 0)]
        [InlineData("https://", 0)]
        [InlineData("ftp://ftpserver.com", 0)]
        [InlineData("http://twitter.com", 1)]
        [InlineData("http://www.twitter.com", 1)]
        [InlineData("http://www.twitter.com/route?query=string", 1)]
        [InlineData("pretext http://twitter.com posttext", 1)]
        [InlineData("http://twitter.com text http://www.twitter.com/route", 2)]
        public void UrlRegex_ProperlyMatchesUrls(string tweetText, int expectedResult)
        {
            int matchCount = RegexHelper.Url.Matches(tweetText).Count;
            Assert.Equal(expectedResult, matchCount);
        }

        [Theory]
        [InlineData("#", 0)]
        [InlineData("pretext#", 0)]
        [InlineData("pre # space", 0)]
        [InlineData("#hashtag", 1)]
        [InlineData("pretext #hashtag", 1)]
        [InlineData("pretext #hashtag posttext", 1)]
        [InlineData("#hashtag posttext", 1)]
        [InlineData("#hashtag text #hashtag2", 2)]
        [InlineData("pretext #hashtag text #hashtag2 posttext", 2)]
        public void HashtagRegex_ProperlyMatchesHashtags(string tweetText, int expectedResult)
        {
            int matchCount = RegexHelper.Hashtag.Matches(tweetText).Count;
            Assert.Equal(expectedResult, matchCount);
        }
    }
}
