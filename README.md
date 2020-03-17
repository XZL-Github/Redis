# Redis 
# RedisHelper 是使用ServiceStack.Redis基本封装常用的api 可以直接调用
# RedisDemo 是对常见的redis的数据结构的一些简单的用法
# RedisService 是对asp.net core DI IServiceCollection的扩展 AddRedisServer
# Asp.net Core Redis asp.net core 使用redis
# asp.net core 使用redis 用法
#  public Startup(IConfiguration configuration)
#        {
#            Configuration = (IConfigurationRoot) configuration;
#        }
#
#        public IConfigurationRoot Configuration { get; }
#
#        // This method gets called by the runtime. Use this method to add services to the container.
#        public void ConfigureServices(IServiceCollection services)
#        {
#            services.AddRedisServer(Configuration);
#            services.AddControllers();
#        }
