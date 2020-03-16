using Redis.Helper.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Linq;

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
            Console.WriteLine("**********RedisString*********");

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
        }
    }
}
