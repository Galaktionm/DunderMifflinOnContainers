using StackExchange.Redis;

namespace Aggregator.Services
{
    public class RedisConnectionService
    {
        public ConnectionMultiplexer connection;
        public RedisConnectionService()
        {
            connection = ConnectionMultiplexer.Connect("redis:6379");
        }
    }
}
