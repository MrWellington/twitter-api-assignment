using System;
using Microsoft.EntityFrameworkCore;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence
{
    public class TwitterClientDbContext : DbContext
    {
        public TwitterClientDbContext(DbContextOptions<TwitterClientDbContext> options) : base(options) { }

        // public DbSet<TopDomains> TopDomains { get; set; }
        // public DbSet<TopEmojis> TopEmojis { get; set; }
        // public DbSet<TopHashtags> TopHashtags { get; set; }
        public DbSet<ProcessedTweet> ProcessedTweets { get; set; }
    }
}
