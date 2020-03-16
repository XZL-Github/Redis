using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Helper.Init
{
    /// <summary>
    /// redis配置文件信息
    /// 也可以放到配置文件去
    /// </summary>
    public sealed class RedisConfigInfo
    {
        /// <summary>
        /// 可写的redis链接地址
        /// 可以多个
        /// 默认端口6379
        /// </summary>
        public string[] WriteServerList = new string[] { "127.0.0.1:6379" };
        /// <summary>
        /// 可读的redis链接地址
        /// 可以多个
        /// 默认端口6379
        /// </summary>
        public string[] ReadServerList = new string[] { "127.0.0.1:6379" };
        /// <summary>
        /// 最大写链接数
        /// </summary>
        public int MaxWritePoolSize = 60;
        /// <summary>
        /// 最大读链接数
        /// </summary>
        public int MaxReadPoolSize = 60;
        /// <summary>
        /// 本地缓存到期时间，单位：秒
        /// </summary>
        public int LocaCacheTime = 180;
        /// <summary>
        /// 是否自动重启
        /// </summary>
        public bool AutoStart = true;
        /// <summary>
        /// 是否记录日志,改设置仅用于排查redis运行时出现的问题
        /// 如redis工作正常,请关闭该项
        /// </summary>
        public bool RecoredLog = true;
    }
}
