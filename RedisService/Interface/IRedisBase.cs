using System;
using System.Collections.Generic;
using System.Text;

namespace RedisService.Interface
{
    public interface IRedisBase
    {
        #region 赋值
        /// <summary>
        /// 设置key的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        bool Set<T>(string key, T value);
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns></returns>
        bool Set<T>(string key, T value, DateTime expiresAt);
        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiresIn">过期时间</param>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan expiresIn);
        /// <summary>
        /// 设置多个key/value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        void Set(Dictionary<string, string> dic);
        #endregion

        #region 追加
        /// <summary>
        /// 在原有key的value值之后加value,没有就新增一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long Append(string key, string value);
        #endregion

        #region 获取值
        /// <summary>
        /// 获取key的value的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<string> Get(List<string> keys);
        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<T> Get<T>(List<string> keys);
        #endregion

        #region 获取旧值赋上新值
        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetAndSetValue(string key, string value);
        #endregion

        #region 辅助方法
        /// <summary>
        /// 获取值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long GetLength(string key);
        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long Incr(string key);
        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">自增数</param>
        /// <returns></returns>
        long IncrBy(string key, int count);
        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long Decr(string key);
        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">自减数</param>
        /// <returns></returns>
        long DecrBy(string key, int count);
        #endregion
    }
}
