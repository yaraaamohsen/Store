namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task SetCacheValueAsync(string key, string value, TimeSpan duration);
        Task<string?> GetCacheValueAsync(string key);
    }
}
