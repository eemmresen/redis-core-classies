using Newtonsoft.Json;

namespace CarrtellWebhook.Core.CacheService.Redis
{
    public class RedisCacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService()
        {
            _redisServer = new RedisServer();
        }

        public void Add(string key, object data,double second)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData);
            _redisServer.Database.KeyExpire(key, DateTime.UtcNow.AddSeconds(second)); 
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void Close()
        {
            _redisServer.CloseDatabase();
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}
