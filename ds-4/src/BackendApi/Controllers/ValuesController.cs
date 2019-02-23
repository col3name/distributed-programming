using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BackendApi.Controllers
{
    public class ValuesController : BaseApiController
    {
        static readonly ConcurrentDictionary<string, string> _data = new ConcurrentDictionary<string, string>();

        [HttpGet]
        public ConcurrentDictionary<string, string> Index()
        {
            return _data;
        }

        // GET api/values/<id>
        [HttpGet("{id}")]
        public string Get(string id)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            return db.StringGet(id);
        }

        [HttpGet("{id}")]
        public string GetTextRank(string id)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            return db.StringGet(id);
        }

        [HttpGet("{id}")]
        public IActionResult TextDetails(string id)
        {
            for (int i = 0; i < 5; i++)
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
                IDatabase db = redis.GetDatabase();

                String value = db.StringGet(id);

                if (value != null)
                {
                    Console.WriteLine("\n\n\n\n\n\n\n\nivalue:" + value + "\n\n\n\n\n\n\n\n");

                    return Ok(value);
                }

                Thread.Sleep(200);
            }

            return NotFound();
        }


        // POST api/values
        [HttpPost]
        public string Post([FromForm]string value)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            var id = Guid.NewGuid().ToString();
            db.StringSet(id, value);
            var publisher = redis.GetSubscriber();
            RedisChannel channel = new RedisChannel("TextCreated", RedisChannel.PatternMode.Pattern);

            var count = publisher.PublishAsync(channel, id);
            Console.WriteLine($"Number of listeners for test {count}");

            // _data[id] = value;
            return id;
        }
    }
}
