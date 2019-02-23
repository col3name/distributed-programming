using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using TextRankCalculate.Models;
using TextRankCalculate.Services;

namespace TextRankCalculate
{
    class Application
    {
        private ITextRankCalculateService _textRankCalculate;

        public Application(ITextRankCalculateService textRankCalculate)
        {
            _textRankCalculate = textRankCalculate;
        }

        public void Run()
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
                    var Id = id.ToString();
                    var Value = db.StringGet(Id);
                    TextRank textRank = _textRankCalculate.calculate(Value);
                    Console.WriteLine(textRank);
                    double result = textRank.CountVowels / textRank.CountConsonants;
                    Console.WriteLine(result.ToString());

                    db.StringSet(Id, result);
                });
            }
        }
    }
}
