using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace MSCourse.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;

        private ConnectionMultiplexer _ConnectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDatabase(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);

        public List<string> GetAllKeys() 
        {
            List<string> listKeys = new List<string>();

            RedisKey[] redisKeys = _ConnectionMultiplexer.GetServer(_host, _port).Keys(database: 1, pattern: "*").ToArray();

            listKeys.AddRange(redisKeys.Select(key => (string)key).ToList());

            return listKeys;
        }


    }
}
