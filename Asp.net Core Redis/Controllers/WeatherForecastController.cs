using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisService.Service;

namespace Asp.net_Core_Redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly RedisStringService _redisStringService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,RedisStringService redisStringService)
        {
            _logger = logger;
            _redisStringService = redisStringService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            using (_redisStringService)
            {
                _redisStringService.FlushAll();
                _redisStringService.Set("hello1", "api1");
                var result1 = _redisStringService.Get("hello1");
            }
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
