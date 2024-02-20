using StackExchange.Redis;

namespace CarrtellWebhook.Core.CacheService.Redis
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString;

        private readonly string host = "your host id";
        private readonly string port = "your port";
        private readonly int databaseId = 0;

        public RedisServer()
        {
            CreateRedisConfigurationString();

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString);
            _database = _connectionMultiplexer.GetDatabase(databaseId);
        }

        public IDatabase Database => _database;

        public void CloseDatabase()
        {
            _connectionMultiplexer.Close();
        }

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(databaseId);
        }

        private void CreateRedisConfigurationString()
        {
            configurationString = $"{host}:{port}";
        }
    }
}
