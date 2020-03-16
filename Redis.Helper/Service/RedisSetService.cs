using Redis.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper.Service
{
    /// <summary>
    /// Set：用哈希表来保持字符串的唯一性，没有先后顺序，存储一些集合性的数据
    /// 1.共同好友、二度好友
    /// 2.利用唯一性，可以统计访问网站的所有独立 IP
    /// </summary>
    public class RedisSetService : RedisBase
    {
        #region 添加
        /// <summary>
        /// key集合中添加value值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            base.iRedisClient.AddItemToSet(key, value);
        }
        /// <summary>
        /// key集合中添加list集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        public void Add(string key, List<string> list)
        {
            base.iRedisClient.AddRangeToSet(key, list);
        }
        #endregion

        #region 获取
        /// <summary>
        /// 随机获取key集合中的一个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRandomItemFormSet(string key)
        {
            return base.iRedisClient.GetRandomItemFromSet(key);
        }
        /// <summary>
        /// 获取所有key集合的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetCount(string key)
        {
            return base.iRedisClient.GetSetCount(key);
        }
        /// <summary>
        /// 获取所有key集合的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HashSet<string> GetAllItemsFormSet(string key)
        {
            return base.iRedisClient.GetAllItemsFromSet(key);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 随机删除key集合中的一个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string RandomRemoveItemFormSet(string key)
        {
            return base.iRedisClient.PopItemFromSet(key);
        }
        /// <summary>
        /// 删除key集合中的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RemoveItemFormSet(string key, string value)
        {
            base.iRedisClient.RemoveItemFromSet(key, value);
        }
        #endregion

        #region 其它
        /// <summary>
        /// 从formkey集合中移除值为value的值，并把value添加到tokey集合中
        /// </summary>
        /// <param name="formkey"></param>
        /// <param name="tokey"></param>
        /// <param name="value"></param>
        public void MoveBetweenSets(string formkey, string tokey, string value)
        {
            base.iRedisClient.MoveBetweenSets(formkey, tokey, value);
        }
        /// <summary>
        /// 返回keys多个集合中的并集,返回hashSet
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetUnionFormSets(params string[] keys)
        {
            return base.iRedisClient.GetUnionFromSets(keys);
        }
        /// <summary>
        /// 返回keys多个集合中的交集，返回hashSet
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetIntersectFromSets(params string[] keys)
        {
            return base.iRedisClient.GetIntersectFromSets(keys);
        }
        /// <summary>
        /// 返回keys多个集合中的差集，返回hashSet
        /// </summary>
        /// <param name="fromkey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public HashSet<string> GetDifferencesFromSet(string fromkey, params string[] keys)
        {
            return base.iRedisClient.GetDifferencesFromSet(fromkey, keys);
        }
        /// <summary>
        /// keys多个集合中的并集，放入newkey集合中
        /// </summary>
        /// <param name="newkey"></param>
        /// <param name="keys"></param>
        public void StoreUnionFormSets(string newkey, string[] keys)
        {
            base.iRedisClient.StoreUnionFromSets(newkey, keys);
        }
        /// <summary>
        /// 把fromkey集合中的数据与keys集合中的数据对比，fromkey集合中不存在keys集合中，则把这些不存在的数据放入newkey集合中
        /// </summary>
        /// <param name="newkey"></param>
        /// <param name="fromkey"></param>
        /// <param name="keys"></param>
        public void StoreDifferencesFromSet(string newkey, string fromkey, string[] keys)
        {
            base.iRedisClient.StoreDifferencesFromSet(newkey, fromkey, keys);
        } 
        #endregion
    }
}
