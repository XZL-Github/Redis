using RedisDemo;
using RedisDemo.RedisSetServiceDemo;
using RedisDemo.RedisZSetServiceDemo;
using System;

namespace My.Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Redis!");
            {
                //RedisDemo1.Show();
            }
            {
                //OversellDemo.Show();
                //OversellField.Show();
            }
            {
                //FriendManager.Show();
            }
            {
                RankManager.Show();
            }

        }
    }
}
