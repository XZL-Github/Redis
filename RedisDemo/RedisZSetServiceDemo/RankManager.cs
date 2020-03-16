using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisDemo.RedisZSetServiceDemo
{
    /// <summary>
    /// 实时排行榜：刷个礼物
    /// 维度很多，平台/房间/主播/日/周/月/年
    /// A对B 刷个礼物，影响很多
    /// 刷礼物只记录流水，不影响排行，凌晨24点跑任务更新
    /// 
    /// 实时排行榜
    /// Redis-IncrementItemInSortedSet
    /// 刷礼物时增加redis分数
    /// 就可以实时获取最新的排行
    /// (多个维度就是多个zSet，刷礼物的时候保存数据库并更新Redis)
    /// </summary>
    public class RankManager
    {
        private static List<string> UserList = new List<string>()
        {
            "Tenk","花生","Ray","阿莫西林","石昊","ywa"
        };
        private static Dictionary<string, int> giftList = new Dictionary<string, int>();
        public static void Show()
        {
            giftList.Add("a", 100);
            giftList.Add("b", 200);
            giftList.Add("c", 300);
            giftList.Add("d", 400);
            giftList.Add("e", 500);
            using (RedisZSetService service = new RedisZSetService())
            {
                service.FlushAll();//清理全部数据
                Task.Run(() =>
                {
                    while (true)
                    {
                        foreach (var user in UserList)
                        {
                            Thread.Sleep(10);
                            service.IncrementItemInSortedSet("陈一发儿", user, new Random().Next(1, 100));//表示在原来刷礼物的基础上增加礼物
                        }
                        Thread.Sleep(20 * 1000);
                    }
                });

                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(12 * 1000);
                        Console.WriteLine("**********当前排行************");
                        int i = 1;

                        foreach (var item in service.GetAllWithScoresFromSortedSet("陈一发儿"))
                        {
                            Console.WriteLine($"第{i++}名 {item.Key} 分数{item.Value}");
                        }
                        //foreach (var item in service.GetAllDesc("陈一发儿"))
                        //{
                        //    Console.WriteLine($"第{i++}名 {item}");
                        //}
                    }
                });

                Console.Read();
            }
        }
    }
}
