using Microsoft.Extensions.Caching.Memory;

namespace Findox.Api.Domain.Helpers
{
    public static class MemoryCacheExtensions
    {
        public static readonly MemoryCacheEntryOptions DefaultMemoryCacheEntryOptions
            = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(10),
                Size = 1
            };

        public static async Task<TObject> GetOrSetValueAsync<TObject>(this IMemoryCache cache, string key, Func<Task<TObject>> factory, MemoryCacheEntryOptions? options = null)
            where TObject : class
        {
            if (cache.TryGetValue(key, out object value))
            {
                return value as TObject;
            }

            var result = await factory();

            options ??= DefaultMemoryCacheEntryOptions;
            cache.Set(key, result, options);

            return result;
        }
    }
}
