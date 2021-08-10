using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.Persistence
{
    public class TwitterClientDbContext : DbContext
    {
        public TwitterClientDbContext(DbContextOptions<TwitterClientDbContext> options) : base(options) { }

        public DbSet<TopDomains> TopDomains { get; set; }
        public DbSet<TopEmojis> TopEmojis { get; set; }
        public DbSet<TopHashtags> TopHashtags { get; set; }
        public DbSet<ProcessedTweet> ProcessedTweets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TopDomains>().HasKey(e => e.Domain);

            modelBuilder.Entity<TopEmojis>().HasKey(e => e.Emoji);

            modelBuilder.Entity<TopHashtags>().HasKey(e => e.Hashtag);
        }
    }
}
