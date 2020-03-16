using Redis.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper.Service
{
    /// <summary>
    /// Hash：类似dictionary，通过索引快速定位到指定元素的，耗时均等，跟string的区别在于不用反序列化，直接修改某个字段
    /// string的话要么是 001:序列化整个实体
    ///           要么是 001_name:  001_pwd: 多个key-value
    /// Hash的话，一个hashid-{key:value;key:value;key:value;}
    /// 可以一次性查找实体，也可以单个，还可以单个修改
    /// </summary>
    public class RedisHashService : RedisBase
    {
        #region 添加
        /// <summary>
        /// 向hashId集合中添加key/value
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetEntryInHash(string hashId, string key, string value)
        {
            return base.iRedisClient.SetEntryInHash(hashId, key, value);
        }
        /// <summary>
        /// 如果hahId集合中存在key/value则不添加返回false反之就添加key/value
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetEntryInHashIfNotExists(string hashId, string key, string value)
        {
            return base.iRedisClient.SetEntryInHashIfNotExists(hashId, key, value);
        }
        /// <summary>
        /// 存储对象T t到hash集合中
        /// 需要包含Id,然后用Id获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void StoreAsHash<T>(T t)
        {
            base.iRedisClient.StoreAsHash<T>(t);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 获取对象T中ID为id的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetFormHash<T>(object id)
        {
            return base.iRedisClient.GetFromHash<T>(id);
        }
        /// <summary>
        /// 获取所有hashId数据集的key/value数据集合
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllEntriesFormHash(string hashId)
        {
            return base.iRedisClient.GetAllEntriesFromHash(hashId);
        }
        /// <summary>
        /// 获取hashId数据集中的数据总数
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public long GetHashCount(string hashId)
        {
            return base.iRedisClient.GetHashCount(hashId);
        }
        /// <summary>
        /// 获取hashId数据集中所有key的集合
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<string> GetHashKeys(string hashId)
        {
            return base.iRedisClient.GetHashKeys(hashId);
        }
        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        public List<string> GetHashValues(string hashId)
        {
            return base.iRedisClient.GetHashValues(hashId);
        }
        /// <summary>
        /// 获取hashId数据集中，key的value数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValueFormHash(string hashId, string key)
        {
            return base.iRedisClient.GetValueFromHash(hashId, key);
        }
        /// <summary>
        /// 获取hashId数据集中，多个key的value集合
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> GetValuesFormHash(string hashId, string[] keys)
        {
            return base.iRedisClient.GetValuesFromHash(hashId, keys);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除hashId数据集中的key数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveEntryFormHash(string hashId, string key)
        {
            return base.iRedisClient.RemoveEntryFromHash(hashId, key);
        }
        #endregion

        #region 其它
        /// <summary>
        /// 判断hashId数据集中是否存在key的数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HashContainsEntry(string hashId, string key)
        {
            return base.iRedisClient.HashContainsEntry(hashId, key);
        }
        /// <summary>
        /// 给hashId数据集key的value加countBy,返回相加后的数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="countBy"></param>
        /// <returns></returns>
        public double IncrementValueInHash(string hashId, string key, double countBy)
        {
            return base.iRedisClient.IncrementValueInHash(hashId, key, countBy);
        } 
        #endregion
    }
}
