using System;

namespace Vanacorps.TwitterClient.Domain
{
    public class ProcessedTweet
    {
        public string ID { get; set; }
        public DateTime ReceivedTime { get; set; }
        public bool ContainsEmojis { get; set; }
        public bool ContainsUrl { get; set; }
        public bool ContainsPhotoUrl { get; set; }
    }
}
