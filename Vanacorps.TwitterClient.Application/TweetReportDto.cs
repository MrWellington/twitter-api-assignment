using System.Collections.Generic;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application
{
    public class TweetReportDto
    {
        public int TotalTweetCount { get; set; }
        public TweetAveragesDto TweetAverages { get; set; }
        public decimal PercentWithEmojis { get; set; }
        public decimal PercentWithUrls { get; set; }
        public decimal PercentWithPhotoUrls { get; set; }
        public List<TopEmojis> TopEmojis { get; set; }
        public List<TopHashtags> TopHashtags { get; set; }
        public List<TopDomains> TopDomains { get; set; }
    }
}
