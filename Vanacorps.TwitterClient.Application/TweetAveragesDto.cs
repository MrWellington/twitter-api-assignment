using System.Collections.Generic;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Application
{
    public class TweetAveragesDto
    {
        public decimal BySecond { get; set; }
        public decimal ByMinute { get; set; }
        public decimal ByHour { get; set; }
    }
}
