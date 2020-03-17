using System;
using System.Collections.Generic;
using System.Text;

namespace RedisService
{
    /// <summary>
    /// redis配置文件信息
    /// 也可以放到配置文件去
    /// </summary>
    public class RedisConfigInfo
    {
        /// <summary>
        /// 可写的redis链接地址
        /// 可以多个
        /// 默认端口6379
        /// </summary>
        //public string[] WriteServerList = new string[] { "127.0.0.1:6379" };
        public string[] WriteServerList { get; set; }
        /// <summary>
        /// 可读的redis链接地址
        /// 可以多个
        /// 默认端口6379
        /// </summary>
        //public string[] ReadServerList = new string[] { "127.0.0.1:6379" };
        public string[] ReadServerList { get; set; }
        /// <summary>
        /// 最大写链接数
        /// </summary>
        //public int MaxWritePoolSize = 60;
        public int MaxWritePoolSize { get; set; }
        /// <summary>
        /// 最大读链接数
        /// </summary>
        //public int MaxReadPoolSize = 60;
        public int MaxReadPoolSize { get; set; }
        /// <summary>
        /// 本地缓存到期时间，单位：秒
        /// </summary>
        //public int LocaCacheTime = 180;
        public int LocaCacheTime { get; set; }
        /// <summary>
        /// 是否自动重启
        /// </summary>
        //public bool AutoStart = true;
        public bool AutoStart { get; set; }
        /// <summary>
        /// 是否记录日志,改设置仅用于排查redis运行时出现的问题
        /// 如redis工作正常,请关闭该项
        /// </summary>
        //public bool RecoredLog = true;
        public bool RecoredLog { get; set; }
    }
}
