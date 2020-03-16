using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDemo.RedisSetServiceDemo
{
    /// <summary>
    /// 好友管理 共同好友-可能认识
    /// 
    /// 找出共同好友: 
    /// 关系型数据库：找出2个好友列表，然后再比对一下
    ///     
    /// 二次好友(可能认识)：
    /// 
    /// </summary>
    public class FriendManager
    {
        public static void Show()
        {
            //去重：IP统计去重；添加好友申请；投票限制；点赞；
            //交叉并的使用
            using (RedisSetService service=new RedisSetService())
            {
                service.FlushAll();

                service.Add("bob", "Powell");
                service.Add("bob", "Tenk");
                service.Add("bob", "spider");
                service.Add("bob", "spider");
                service.Add("bob", "spider");
                service.Add("bob", "aaron");
                service.Add("bob", "Linsan");

                service.Add("Powell", "bob");
                service.Add("Powell", "Tenk");
                service.Add("Powell", "ywa");
                service.Add("Powell", "Pang");
                service.Add("Powell", "Jeff");

                var result1=service.GetIntersectFromSets("bob", "Powell");
                var result2 = service.GetDifferencesFromSet("Powell", "bob");
                var result3 = service.GetDifferencesFromSet("bob", "Powell");
                var result4 = service.GetUnionFormSets("Powell", "bob");
            }
        }
    }
}
