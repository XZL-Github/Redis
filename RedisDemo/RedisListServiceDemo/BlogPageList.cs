using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RedisDemo.RedisListServiceDemo
{
    /// <summary>
    /// ask项目，问答  一天的问题都是几万，表里面是几千万数据
    /// 首页要展示最新的问题，ajax动态定时获取刷新
    /// 还有前20页是很多人访问的
    /// 
    /// 每次写入数据库的时候，把ID_标题写入到Redis的List
    /// (后面搞个TrimList,只要最近的200个)
    /// 用户刷页面就不需要去数据库，直接redis
    /// 
    /// 还有一种就是水平分表了，
    /// 第一次的时候可以不管分页，只拿最新数据
    /// 存数据的时候可以保存id+表名称
    /// 
    /// 主要是解决数据量大，变化快的数据分页问题，
    /// 二八原则，80%的访问集中在20%的数据，list里面只用保存大概的量就够用了
    /// 
    /// </summary>
    public class BlogPageList
    {
        public static void Show()
        {
            using (RedisListService service = new RedisListService())
            {
                service.RPush("newBlog", "11231_1fsgdfgd");
                service.RPush("newBlog", "41233_3fdfsfdsf");
                service.RPush("newBlog", "12345_4dsdsdsd");
                service.RPush("newBlog", "12354_2dsadsaf");
                service.RPush("newBlog", "12343_3fdsfsdfsdf");
                service.RPush("newBlog", "12323_2fsdfsdfsdfsd");

                service.TrimList("newBlog", 0, 200);//一个list最多2的32次方-1
                service.Get("newBlog", 0, 9);
                //后面的页也就是在这里获取
                service.Get("newBlog", 10, 19);
                service.Get("newBlog", 20, 29);
            }
        }

        private static string InsertDB(object blog)
        {
            Thread.Sleep(200);
            return new Guid().ToString();
        }

        private static List<object> FindPage()
        {
            Thread.Sleep(1200);
            return new List<object>();
        }
    }
}
