using Redis.Helper.Interface;
using System;
using System.Collections.Generic;

namespace Redis.Helper.Service
{
    /// <summary>
    /// key-value 键值对:value可以是序列化的数据
    /// </summary>
    public class RedisStringService : RedisBase
    {
        #region 赋值
        /// <summary>
        /// 设置key的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value)
        {
            return base.iRedisClient.Set<T>(key, value);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            return base.iRedisClient.Set<T>(key, value, expiresAt);
        }
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiresIn">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return base.iRedisClient.Set<T>(key, value, expiresIn);
        }
        /// <summary>
        /// 设置多个key/value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public void Set(Dictionary<string, string> dic)
        {
            base.iRedisClient.SetAll(dic);
        }
        #endregion

        #region 追加
        /// <summary>
        /// 在原有key的value值之后加value,没有就新增一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Append(string key, string value)
        {
            return base.iRedisClient.AppendToValue(key, value);
        }
        #endregion

        #region 获取值
        /// <summary>
        /// 获取key的value的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return base.iRedisClient.GetValue(key);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> Get(List<string> keys)
        {
            return base.iRedisClient.GetValues(keys);
        }
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<T> Get<T>(List<string> keys)
        {
            return base.iRedisClient.GetValues<T>(keys);
        }
        #endregion

        #region 获取旧值赋上新值
        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetAndSetValue(string key, string value)
        {
            return base.iRedisClient.GetAndSetValue(key, value);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetLength(string key)
        {
            return base.iRedisClient.GetStringCount(key);
        }
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Incr(string key)
        {
            return base.iRedisClient.IncrementValue(key);
        }
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">自增数</param>
        /// <returns></returns>
        public long IncrBy(string key, int count)
        {
            return base.iRedisClient.IncrementValueBy(key, count);
        }
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long Decr(string key)
        {
            return base.iRedisClient.DecrementValue(key);
        }
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">自减数</param>
        /// <returns></returns>
        public long DecrBy(string key, int count)
        {
            return base.iRedisClient.DecrementValueBy(key, count);
        } 
        #endregion
    }
}
