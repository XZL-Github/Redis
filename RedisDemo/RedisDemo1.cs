using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace RedisDemo
{
    public class RedisDemo1
    {
        public static void Show()
        {
            Student student1 = new Student()
            {
                Id = 1,
                Name = "bob",
                Remark = "测试1",
            };
            Student student2 = new Student()
            {
                Id = 2,
                Age = 18,
                Name = "alice",
                Remark = "测试2"
            };
            Console.WriteLine("**********Redis*********");

            using (RedisStringService service = new RedisStringService())
            {
                service.FlushAll();
                service.Set("RedisStringService_key1", "RedisStringService_value1");
                Console.WriteLine(service.Get("RedisStringService_key1"));
                service.GetAndSetValue("RedisStringService_key1", "RedisStringService_value2");
                Console.WriteLine(service.Get("RedisStringService_key1"));

                service.Append("RedisStringService_key1", "Append");
                Console.WriteLine(service.Get("RedisStringService_key1"));
                service.Set("RedisStringService_key1", "RedisStringService_value", new TimeSpan(0, 0, 0, 5));//5秒过期
                Console.WriteLine(service.Get("RedisStringService_key1"));
                Thread.Sleep(5000);//休息5秒
                Console.WriteLine(service.Get("RedisStringService_key1"));
            }
            //序列化对象
            using (RedisStringService service = new RedisStringService())
            {
                service.FlushAll();
                service.Set("Student_1", JsonConvert.SerializeObject(student1));
                Console.WriteLine(service.Get("Student_1"));
                //var result1 = JsonConvert.DeserializeObject<Student>(service.Get("Student_1"));
                //Console.WriteLine(result1.Age);
                service.Set<Student>("Student_2", student2);
                Console.WriteLine(service.Get("Student_2"));
                var result2 = service.Get<Student>(new List<string>() { "Student_2" });
                Console.WriteLine(result2.FirstOrDefault().Name);
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("student_1", "student-1111");
                keyValuePairs.Add("student_2", "student-2222");
                service.Set(keyValuePairs);
                var result3 = service.Get(keyValuePairs.Keys.ToList());
                foreach (var item in result3)
                {
                    Console.WriteLine(item);
                }
            }

            using (RedisStringService service = new RedisStringService())
            {
                service.FlushAll();
                service.Set<string>("student1", "梦的翅膀");
                Console.WriteLine(service.Get("student1"));

                service.Append("student1", "20180802");
                Console.WriteLine(service.Get("student1"));

                Console.WriteLine(service.GetAndSetValue("student1", "程序错误"));
                Console.WriteLine(service.Get("student1"));

                service.Set<string>("student2", "王", DateTime.Now.AddSeconds(5));
                Thread.Sleep(5100);
                Console.WriteLine(service.Get("student2"));

                service.Set<int>("Age", 32);
                Console.WriteLine(service.Incr("Age"));
                Console.WriteLine(service.IncrBy("Age", 3));
                Console.WriteLine(service.Decr("Age"));
                Console.WriteLine(service.DecrBy("Age", 3));
            }

            using (RedisHashService service = new RedisHashService())
            {
                service.FlushAll();
                service.SetEntryInHash("student", "id", "123456");
                service.SetEntryInHash("student", "name", "张xx");
                service.SetEntryInHash("student", "age", "18");
                service.SetEntryInHash("student", "remark", "哈哈哈");

                var result1 = service.GetHashKeys("student");
                var result2 = service.GetHashValues("student");
                var result3 = service.GetAllEntriesFormHash("student");
                var result4 = service.GetValueFormHash("student", "id");
                var result5 = service.SetEntryInHashIfNotExists("student", "name", "詹飒");
                var result6 = service.SetEntryInHashIfNotExists("student", "files", "您好");
                service.StoreAsHash<Student>(student2);//含ID才可以的
                var result7 = service.GetFromHash<Student>(student2.Id);//含ID才可以的
                Console.WriteLine();

            }

            Console.WriteLine("**********RedisSet*********");
            using (RedisSetService service = new RedisSetService())
            {
                service.FlushAll();

                service.Add("set1", "1");
                service.Add("set1", "1");
                service.Add("set1", "1");
                service.Add("set1", "1");
                service.Add("set1", "1");
                service.Add("set1", "2");
                service.Add("set1", "3");
                service.Add("set1", "7");
                service.Add("set1", "7");
                service.Add("set1", "7");
                service.Add("set1", "5");

                var result1 = service.GetAllItemsFormSet("set1");
                var result2 = service.GetRandomItemFormSet("set1");
                var result3 = service.GetCount("set1");
                service.RemoveItemFormSet("set1", "1");
                {
                    service.Add("begin", "111");
                    service.Add("begin", "112");
                    service.Add("begin", "115");
                    service.Add("begin", "116");
                    service.Add("begin", "117");

                    service.Add("end", "111");
                    service.Add("end", "112");
                    service.Add("end", "115");
                    service.Add("end", "114");
                    service.Add("end", "113");

                    var result4 = service.GetIntersectFromSets("begin", "end");
                    var result5 = service.GetDifferencesFromSet("begin", "end");
                    var result6 = service.GetUnionFormSets("begin", "end");
                }

            }

            Console.WriteLine("**********RedisZSet*********");
            using (RedisZSetService service = new RedisZSetService())
            {
                service.FlushAll();//清理全部数据

                service.Add("advanced", "1");
                service.Add("advanced", "2");
                service.Add("advanced", "5");
                service.Add("advanced", "4");
                service.Add("advanced", "7");
                service.Add("advanced", "5");
                service.Add("advanced", "9");

                var result1 = service.GetAll("advanced");
                var result2 = service.GetAllDesc("advanced");

                service.AddItemToSortedSet("Sort", "BY", 123234);
                service.AddItemToSortedSet("Sort", "走自己的路", 123);
                service.AddItemToSortedSet("Sort", "redboy", 45);
                service.AddItemToSortedSet("Sort", "大蛤蟆", 7567);
                service.AddItemToSortedSet("Sort", "路人甲", 9879);
                service.AddRangeToSortedSet("Sort", new List<string>() { "123", "花生", "加菲猫" }, 3232);
                var result3 = service.GetAllWithScoresFromSortedSet("Sort");

                //交叉并
            }


            Console.WriteLine("********** RedisList*********");
            {
                //using (RedisListService service = new RedisListService())
                //{
                //    service.FlushAll();

                //    //service.Add("article", "eleven1234");
                //    //service.Add("article", "kevin");
                //    //service.Add("article", "大叔");
                //    //service.Add("article", "C卡");
                //    //service.Add("article", "触不到的线");
                //    //service.Add("article", "程序错误");
                //    service.RPush("article", "eleven1234");
                //    service.RPush("article", "kevin");
                //    service.RPush("article", "大叔");
                //    service.RPush("article", "C卡");
                //    service.RPush("article", "触不到的线");
                //    service.RPush("article", "程序错误");

                //    var result1 = service.Get("article");
                //    var result2 = service.Get("article", 0, 3);
                //    //可以按照添加顺序自动排序；而且可以分页获取

                //    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                //    //栈
                //    service.FlushAll();
                //    service.Add("article", "eleven1234");
                //    service.Add("article", "kevin");
                //    service.Add("article", "大叔");
                //    service.Add("article", "C卡");
                //    service.Add("article", "触不到的线");
                //    service.Add("article", "程序错误");

                //    for (int i = 0; i < 5; i++)
                //    {
                //        Console.WriteLine(service.PopItemFromList("article"));
                //        var result3 = service.Get("article");
                //    }
                //    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                //    // 队列：生产者消费者模型   
                //    service.FlushAll();
                //    service.RPush("article", "eleven1234");
                //    service.RPush("article", "kevin");
                //    service.RPush("article", "大叔");
                //    service.RPush("article", "C卡");
                //    service.RPush("article", "触不到的线");
                //    service.RPush("article", "程序错误");

                //    for (int i = 0; i < 5; i++)
                //    {
                //        Console.WriteLine(service.PopItemFromList("article"));
                //        var result4 = service.Get("article");
                //    }
                //    //分布式缓存，多服务器都可以访问到，多个生产者，多个消费者，任何产品只被消费一次
                //}

                #region 生产者消费者
                //using (RedisListService service = new RedisListService())
                //{
                //    service.Add("test", "这是一个学生Add1");
                //    service.Add("test", "这是一个学生Add2");
                //    service.Add("test", "这是一个学生Add3");

                //    service.LPush("test", "这是一个学生LPush1");
                //    service.LPush("test", "这是一个学生LPush2");
                //    service.LPush("test", "这是一个学生LPush3");
                //    service.LPush("test", "这是一个学生LPush4");
                //    service.LPush("test", "这是一个学生LPush5");
                //    service.LPush("test", "这是一个学生LPush6");

                //    service.RPush("test", "这是一个学生RPush1");
                //    service.RPush("test", "这是一个学生RPush2");
                //    service.RPush("test", "这是一个学生RPush3");
                //    service.RPush("test", "这是一个学生RPush4");
                //    service.RPush("test", "这是一个学生RPush5");
                //    service.RPush("test", "这是一个学生RPush6");

                //    List<string> stringList = new List<string>();
                //    for (int i = 0; i < 10; i++)
                //    {
                //        stringList.Add(string.Format($"放入任务{i}"));
                //    }
                //    service.Add("task", stringList);

                //    Console.WriteLine(service.Count("test"));
                //    Console.WriteLine(service.Count("task"));
                //    var list = service.Get("test");
                //    list = service.Get("task", 2, 4);

                //    Action act = new Action(() =>
                //    {
                //        while (true)
                //        {
                //            Console.WriteLine("************请输入数据**************");
                //            string testTask = Console.ReadLine();
                //            service.LPush("test", testTask);
                //        }
                //    });
                //    act.EndInvoke(act.BeginInvoke(null, null));
                //}
                #endregion

                #region 发布订阅:观察者，一个数据源，多个接受者，只要订阅了就可以收到的，能被多个数据源共享
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Eleven", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{1}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Eleven");
                        });//blocking
                    }
                });
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Eleven", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{2}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Eleven");
                        });//blocking
                    }
                });
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Twelve", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{3}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Twelve");
                        });//blocking
                    }
                });
                using (RedisListService service = new RedisListService())
                {
                    Thread.Sleep(1000);

                    service.Publish("Eleven", "Eleven123");
                    service.Publish("Eleven", "Eleven234");
                    service.Publish("Eleven", "Eleven345");
                    service.Publish("Eleven", "Eleven456");

                    service.Publish("Twelve", "Twelve123");
                    service.Publish("Twelve", "Twelve234");
                    service.Publish("Twelve", "Twelve345");
                    service.Publish("Twelve", "Twelve456");
                    Console.WriteLine("**********************************************");

                    service.Publish("Eleven", "exit");
                    service.Publish("Eleven", "123Eleven");
                    service.Publish("Eleven", "234Eleven");
                    service.Publish("Eleven", "345Eleven");
                    service.Publish("Eleven", "456Eleven");

                    service.Publish("Twelve", "exit");
                    service.Publish("Twelve", "123Twelve");
                    service.Publish("Twelve", "234Twelve");
                    service.Publish("Twelve", "345Twelve");
                    service.Publish("Twelve", "456Twelve");
                }
                //观察者模式：微信订阅号---群聊天---数据同步--
                //MSMQ---RabbitMQ---ZeroMQ---RedisList:学习成本 技术成本
                #endregion
            }
        }
    }
}
