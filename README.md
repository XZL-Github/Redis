# Redis 
+ RedisHelper 是使用ServiceStack.Redis基本封装常用的api 可以直接调用
+ RedisDemo 是对常见的redis的数据结构的一些简单的用法
+ RedisService 是对asp.net core DI IServiceCollection的扩展 AddRedisServer
+ Asp.net Core Redis asp.net core 使用redis
+ asp.net core 使用redis 用法

Startup
```  
        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot) configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRedisServer(Configuration);
            services.AddControllers();
        }
```
appsettings.json
```
       "RedisOptions": {
           "WriteServerList": [ "127.0.0.1:6379" ],
           "ReadServerList": [ "127.0.0.1:6379" ],
           "MaxWritePoolSize": 120,
           "MaxReadPoolSize": 60,
           "LocaCacheTime": 180,
           "AutoStart": "true",
           "RecoredLog": "false"
         }
```
Controller
```
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
```
