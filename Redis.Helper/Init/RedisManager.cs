using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper.Init
{
    /// <summary>
    /// redis管理中心
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfigInfo redisConfigInfo = new RedisConfigInfo();
        /// <summary>
        /// redis客户端池化管理
        /// </summary>
        private static PooledRedisClientManager pooledRedisClientManager;
        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
       private static void CreateManager()
        {
            string[] writeServerConStr = redisConfigInfo.WriteServerList;
            string[] readServerConStr = redisConfigInfo.ReadServerList;
            pooledRedisClientManager = new PooledRedisClientManager(readServerConStr, writeServerConStr,
                new RedisClientManagerConfig
                {
                    MaxWritePoolSize= redisConfigInfo.MaxWritePoolSize,
                    MaxReadPoolSize=redisConfigInfo.MaxReadPoolSize,
                    AutoStart=redisConfigInfo.AutoStart
                });
        }
        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetClient()
        {
            return pooledRedisClientManager.GetClient();
        }
    }
}
