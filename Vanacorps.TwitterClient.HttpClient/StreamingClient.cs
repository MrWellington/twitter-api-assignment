using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.HttpClient
{
    public class StreamingClient : BackgroundService
    {
        private readonly ILogger<StreamingClient> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configProvider;
        private readonly IBus _bus;
        private const int MAX_BACKOFF_INTERVAL_SECONDS = 32;
        private int retrySeconds = 2;

        // Class to manage streaming client through the lifecycle of the application.
        public StreamingClient(ILogger<StreamingClient> logger, IHttpClientFactory clientFactory, IConfiguration configProvider, IBus bus)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configProvider = configProvider;
            _bus = bus;
        }

        // Establish the Twitter client connection. Called on startup if this class is registered with 'AddHostedService' via DI.
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initialize Twitter streaming API client.");

            var twitterClient = GetHttpClient();

            while (!cancellationToken.IsCancellationRequested)
            {
                var httpRequest = GetHttpRequestMessage();

                try
                {
                    _logger.LogInformation("Creating the streaming connection.");
                    var response = await twitterClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Twitter API returned {response.StatusCode}. Retrying in {retrySeconds} seconds.");
                        await DelayAndRetry(cancellationToken);
                        continue;
                    }

                    // Reset the retry timer if we've had a success.
                    retrySeconds = 2;

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            while (!reader.EndOfStream)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                {
                                    break;
                                }

                                var sampledTweetJson = await reader.ReadLineAsync();
                                Tweet sampledTweet = JsonConvert.DeserializeObject<Tweet>(sampledTweetJson);

                                if (sampledTweet == null)
                                {
                                    continue;
                                }

                                await _bus.Publish(sampledTweet);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Exception occurred establishing a connection to the streaming API. Retrying in {retrySeconds} seconds.");
                    await DelayAndRetry(cancellationToken);
                    continue;
                }
            }
        }

        private System.Net.Http.HttpClient GetHttpClient()
        {
            var client = _clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

            return client;
        }

        private HttpRequestMessage GetHttpRequestMessage()
        {
            var streamEndpoint = _configProvider["STREAM_ENDPOINT"];
            var bearerToken = _configProvider["BEARER_TOKEN"];

            if (string.IsNullOrWhiteSpace(streamEndpoint) || string.IsNullOrWhiteSpace(bearerToken))
            {
                // Fail fast if these important values are not present
                _logger.LogCritical("Twitter client configuration values unavailable. STREAM_ENDPOINT and BEARER_TOKEN env variables must be set");
                throw new Exception("Application is unavailable. Please try again later.");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, streamEndpoint);
            request.Headers.Add("Authorization", $"Bearer {bearerToken}");
            request.Headers.Add("User-Agent", "Vanacorps.TwitterClient");

            return request;
        }

        private async Task DelayAndRetry(CancellationToken cancellationToken)
        {
            if (retrySeconds < MAX_BACKOFF_INTERVAL_SECONDS)
            {
                retrySeconds *= 2;
            }

            await Task.Delay(retrySeconds * 1000, cancellationToken);
        }
    }
}

