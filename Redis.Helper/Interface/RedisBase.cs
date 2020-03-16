using Redis.Helper.Init;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper.Interface
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口,主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        public IRedisClient iRedisClient { get; private set; }
        /// <summary>
        /// 构造是完成链接的打开
        /// </summary>
        public RedisBase()
        {
            iRedisClient = RedisManager.GetClient();
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposed)
        {
            if (!this._disposed)
            {
                if (disposed)
                {
                    iRedisClient.Dispose();
                    iRedisClient = null;
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
            using (IRedisTransaction iRedisTransaction=this.iRedisClient.CreateTransaction())
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
            iRedisClient.FlushAll();
        }
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            iRedisClient.Save();//阻塞式save
        }
        /// <summary>
        /// 异步保存数据DB到文件硬盘
        /// </summary>
        public void SaveAsync()
        {
            iRedisClient.SaveAsync();//异步save
        }
    }
}
