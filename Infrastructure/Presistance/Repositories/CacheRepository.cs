﻿using System.Text.Json;
using Domain.Contracts;
using StackExchange.Redis;

namespace Persistance.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return !value.IsNullOrEmpty ? value : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var redisValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, redisValue, duration);
        }
    }
}
