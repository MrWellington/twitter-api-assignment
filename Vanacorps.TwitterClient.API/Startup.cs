using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Vanacorps.TwitterClient.Application.Commands;
using Vanacorps.TwitterClient.Application.Contracts;
using Vanacorps.TwitterClient.Domain;
using Vanacorps.TwitterClient.HttpClient;
using Vanacorps.TwitterClient.Persistence;
using Vanacorps.TwitterClient.Persistence.Repositories;

namespace Vanacorps.TwitterClient.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use MassTransit for messaging abstraction; loose coupling between stream listener and tweet processor
            services.AddMassTransit(x => 
            {
                x.AddConsumer<TweetProcessor.Processor>();

                // Messaging abstraction configured here
                x.UsingInMemory((ctx, cfg) => 
                {
                    cfg.ConfigureEndpoints(ctx);
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vanacorps.TwitterClient.API", Version = "v1" });
            });
            services.AddHttpClient();
            services.AddMassTransitHostedService(true);

            services.AddDbContext<TwitterClientDbContext>(opt => opt.UseInMemoryDatabase("TwitterStatisticsDb"));

            services.AddTransient<IProcessTweetCommand, ProcessTweetCommand>();
            services.AddTransient<IUpdateTopDomainsCommand, UpdateTopDomainsCommand>();
            services.AddTransient<IUpdateTopEmojisCommand, UpdateTopEmojisCommand>();
            services.AddTransient<IUpdateTopHashtagsCommand, UpdateTopHashtagsCommand>();

            services.AddTransient<IProcessedTweetRepository, ProcessedTweetRepository>();
            services.AddTransient<ITopDomainsRepository, TopDomainsRepository>();
            services.AddTransient<ITopEmojisRepository, TopEmojisRepository>();
            services.AddTransient<ITopHashtagsRepository, TopHashtagsRepository>();

            // Register stream client as a hosted service that will live through the application lifecycle
            services.AddHostedService<StreamingClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Configure method");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vanacorps.TwitterClient.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
