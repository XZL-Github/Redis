using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDemo.RedisListServiceDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class SellAsyncTest
    {
        public static void Show()
        {
            //没有队列
            //List<string> intList = new List<string>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    intList.Add($"任务{i}");
            //}
            #region 
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    ParallelOptions parallelOptions = new ParallelOptions();
            //    parallelOptions.MaxDegreeOfParallelism = 10;
            //    Parallel.ForEach(intList, parallelOptions, s =>
            //    {
            //        Thread.Sleep(100);
            //    });
            //    stopwatch.Stop();
            //    Console.WriteLine($"一下子来了1000个任务，处理速度必变，时间得需要{stopwatch.ElapsedMilliseconds}ms");
            //    Console.WriteLine("在这期间，任务又来了好几万。任务累计。根本处理不过来");
            //    //处理能力有限，任务一下子特别多，会出现任务的累计
            //}
            #endregion

            #region 
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
            //Console.WriteLine($"这里是 {tag} 启动了。。");
            //Task.Run(() =>
            //{
            //    using (RedisListService service = new RedisListService())
            //    {
            //        int k = 1;
            //        while (true)
            //        {
            //            List<string> stringList = new List<string>();
            //            for (int i = 0; i < 10; i++)
            //            {
            //                stringList.Add(string.Format($"{tag}放入任务{i}"));
            //            }
            //            service.Add("task", stringList);

            //            service.RPush("test", $"{tag} {k}这是一个学生RPush1");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush2");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush3");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush4");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush5");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush6");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush7");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush8");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush9");
            //            service.RPush("test", $"{tag} {k}这是一个学生RPush10");
            //            Thread.Sleep(1000);
            //            k++;
            //        }
            //    }
            //});
            #endregion

        }

    }
}
