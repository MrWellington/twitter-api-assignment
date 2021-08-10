using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vanacorps.TwitterClient.Application;
using Vanacorps.TwitterClient.Domain;

namespace Vanacorps.TwitterClient.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetReportController : ControllerBase
    {
        private readonly ILogger<TweetReportController> _logger;

        public TweetReportController(ILogger<TweetReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public TweetReportDto Get()
        {
            return new TweetReportDto
            {
                TotalTweetCount = 11
            };
        }
    }
}
