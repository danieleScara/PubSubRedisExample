using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace PubSubExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRedisClient client = new RedisClient()) {
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
            }
        }
    }
}
