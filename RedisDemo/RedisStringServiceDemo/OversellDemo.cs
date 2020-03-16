using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisDemo.RedisStringServiceDemo
{
    /// <summary>
    ///  超卖：订单数超过商品
    ///  数据库：秒杀的时候，10件商品，100个人想买，假定大家一瞬间都来了
    ///  A 查询还有没有--有---1更新
    ///  B 查询还有没有--有---1更新
    ///  C 查询还有没有--有---1更新
    ///  可能会卖出12  12甚至20件商品
    ///  
    ///  所以用上了Redis，一方面保证绝对不会超卖，
    ///                 另一方面没有效率影响，数据库是可以为成功的人并发的
    ///                 还有撤单的时候增加库存，可以继续秒杀，
    ///                 限制秒杀的库存是放在redis，不是数据库，不会造成数据的不一致性
    ///  Redis能够拦截无效的请求，如果没有这一层，所有的请求压力都到数据库
    /// </summary>
    public class OversellDemo
    {
        private static bool IsGoOn = true;//秒杀活动是否结束
        public static void Show()
        {
            using (RedisStringService service = new RedisStringService())
            {
                service.FlushAll();
                service.Set<int>("Stock", 10);//库存
            }
            for (int i = 0; i < 5000; i++)//开启5000个线程
            {
                int k = i;
                Task.Run(() =>
                {
                    using (RedisStringService service = new RedisStringService())
                    {
                        if (IsGoOn)
                        {
                            long index = service.Decr("Stock"); //-1并且返回
                            if (index >= 0)
                            {
                                Console.WriteLine($"{k.ToString("000")}秒杀成功，秒杀商品索引为{index}");
                                //可以分队列，去数据库操作
                            }
                            else
                            {
                                if (IsGoOn)
                                {
                                    IsGoOn = false;
                                }
                                Console.WriteLine($"{k.ToString("000")}秒杀失败，秒杀商品索引为{index}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{k.ToString("000")}秒杀停止......");
                        }
                    }
                });
            }
            Console.Read();
        }
    }

    public class OversellField
    {
        private static bool IsGoOn = true;//秒杀活动是否结束
        private static int Stock;
        public static void Show()
        {
            Stock = 10;
            for (int i = 0; i < 5000; i++)//开启5000个线程
            {
                int k = i;
                Task.Run(() =>
                {

                    if (IsGoOn)
                    {
                        long index = Stock; //-1并且返回
                        Thread.Sleep(100);
                        if (index >= 1)
                        {
                            Stock = Stock - 1;
                            Console.WriteLine($"{k.ToString("000")}秒杀成功，秒杀商品索引为{index}");
                            //可以分队列，去数据库操作
                        }
                        else
                        {
                            if (IsGoOn)
                            {
                                IsGoOn = false;
                            }
                            Console.WriteLine($"{k.ToString("000")}秒杀失败，秒杀商品索引为{index}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{k.ToString("000")}秒杀停止......");
                    }

                });
            }
            Console.Read();
        }
    }
}
