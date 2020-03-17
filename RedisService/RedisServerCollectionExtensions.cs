using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using RedisService;
using ServiceStack.Redis;
using RedisService.Service;
using RedisService.Interface;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisServerCollectionExtensions
    {
        /// <summary>
        /// 可以自定义配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="steup"></param>
        public static void AddRedisServer(this IServiceCollection services, IConfigurationRoot configuration,Action<RedisConfigInfo> steup)
        {
            services.Configure(steup);
            services.Configure<RedisConfigInfo>(options =>
            {
                configuration.GetSection("RedisOptions").Bind(options);
            });
            services.AddSingleton<IRedisClient>(sp =>
            {
                var options = sp.GetService<IOptions<RedisConfigInfo>>()?.Value ?? throw new ArgumentNullException(nameof(RedisConfigInfo));
                return new PooledRedisClientManager(options.WriteServerList, options.ReadServerList,
                    new RedisClientManagerConfig
                    {
                        MaxWritePoolSize = options.MaxWritePoolSize,
                        MaxReadPoolSize = options.MaxReadPoolSize,
                        AutoStart = options.AutoStart
                    }).GetClient();
            });
            services.AddSingleton<IRedisStringService, RedisStringService>();
            services.AddSingleton(typeof(RedisHashService));
            services.AddSingleton(typeof(RedisSetService));
            services.AddSingleton(typeof(RedisZSetService));
            services.AddSingleton(typeof(RedisListService));
        }
        /// <summary>
        /// 默认的
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRedisServer(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<RedisConfigInfo>(options =>
            {
                configuration.GetSection("RedisOptions").Bind(options);
            });
            services.AddSingleton<IRedisClient>(sp =>
            {
                var options = sp.GetService<IOptions<RedisConfigInfo>>()?.Value ?? throw new ArgumentNullException(nameof(RedisConfigInfo));
                 return new PooledRedisClientManager(options.WriteServerList,options.ReadServerList,
                     new RedisClientManagerConfig 
                     {
                         MaxWritePoolSize=options.MaxWritePoolSize,
                         MaxReadPoolSize = options.MaxReadPoolSize,
                         AutoStart=options.AutoStart
                     }).GetClient();
            });
            services.AddSingleton(typeof(RedisStringService));
            services.AddSingleton(typeof(RedisHashService));
            services.AddSingleton(typeof(RedisSetService));
            services.AddSingleton(typeof(RedisZSetService));
            services.AddSingleton(typeof(RedisListService));
        }
    }
}
