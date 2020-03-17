
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisService.Interface
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口,主要用于释放内存
    /// </summary>
    public abstract class RedisBase :IDisposable
    {
        public IRedisClient _IRedisClient { get; set; }
        /// <summary>
        /// 构造是完成链接的打开
        /// </summary>
        public RedisBase(IRedisClient redisClient)
        {
            _IRedisClient = redisClient;
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposed)
        {
            if (!this._disposed)
            {
                if (disposed)
                {
                    _IRedisClient.Dispose();
                    _IRedisClient = null;
                }
            }
            this._disposed = true;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public void Transcation()
        {
            using (IRedisTransaction iRedisTransaction=this._IRedisClient.CreateTransaction())
            {
                try
                {
                    iRedisTransaction.QueueCommand(e => e.Set("key", 20));
                    iRedisTransaction.QueueCommand(e => e.Increment("key", 1));
                    iRedisTransaction.Commit();//提交事务
                }
                catch (Exception ex)
                {
                    iRedisTransaction.Rollback();
                    throw ex;
                }

            }
        }
        /// <summary>
        /// 清除全部数据 请小心
        /// </summary>
        public virtual void FlushAll()
        {
            _IRedisClient.FlushAll();
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            _IRedisClient.Save();//阻塞式save
        }
        /// <summary>
        /// 异步保存数据DB到文件硬盘
        /// </summary>
        public void SaveAsync()
        {
            _IRedisClient.SaveAsync();//异步save
        }
    }
}
