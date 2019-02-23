using StackExchange.Redis;
using System;

namespace TextListener
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer
                    .Connect("localhost");
                IDatabase db = redis.GetDatabase();
                var subscriber = redis.GetSubscriber();
                var redisChannel = new RedisChannel("TextCreated",
                    RedisChannel.PatternMode.Pattern);

                subscriber.SubscribeAsync(redisChannel, (channel, id) =>
                {
                    Console.WriteLine(id.ToString() + " : "
                        + db.StringGet(id.ToString()));
                });
            }
        }
    }
}
