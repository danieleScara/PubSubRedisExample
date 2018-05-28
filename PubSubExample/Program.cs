using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace PubSubExample
{

    public class Sample
    {
        public string prop1;
        public int prop2;
        public Sample(string prop1, int prop2)
        {
            this.prop1 = prop1;
            this.prop2 = prop2;
        }
    }

    class Program
    {

        

        static void Main(string[] args)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            var db = redis.GetDatabase();
            
            db.HashSet("key", new HashEntry[] {
                new HashEntry("key1", JsonConvert.SerializeObject(new Sample("value1",1))),
                new HashEntry("key2", JsonConvert.SerializeObject(new Sample("value2",2))),
            });

            var test = db.HashGetAll("key");

            foreach (var item in test) {
                Console.WriteLine("Value: "+JsonConvert.DeserializeObject(item.Value));
            }

            

            Console.ReadLine();

            /*using (IRedisClient client = new RedisClient()) {
                var subscription = client.CreateSubscription();

                subscription.OnSubscribe = channel =>
                {
                    Console.WriteLine("New subscription to channel {0}", channel);
                };

                subscription.OnUnSubscribe = channel =>
                {
                    Console.WriteLine("Unsubscribed from channel {0}", channel);
                };

                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine("Received message {0} on channel {1}", msg, channel);
                };

                subscription.SubscribeToChannels("test");
            }*/
        }
    }
}
