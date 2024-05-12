using System.Security.Cryptography;
using System.Text;

namespace TinyURL.Files;

public class UrlShorteningService
{
    private const int ShortUrlLen = 8;
    private const int CacheCapacity = 100;
    private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private readonly CacheService<string, string> _cache;

    private readonly MongoDb _db;

    public UrlShorteningService(MongoDb db)
    {
        _db = db;
        _cache = new CacheService<string, string>(CacheCapacity);
    }

    public async Task<string> ShortenUrlAsync(string longUrl)
    {
        if (_cache.TryGetValue(longUrl, out var shortUrl)) 
        {
            return shortUrl;
        }

        shortUrl = await _db.GetShortUrlAsync(longUrl);
        if (shortUrl == null) 
        {
            shortUrl = GenerateShortUrl(longUrl);
            await _db.AddToMapAsync(new Url { ShortUrl = shortUrl, LongUrl = longUrl });
        }
        
        _cache.Add(longUrl, shortUrl);
        return shortUrl;
    }

    public async Task<string> GetLongUrlAsync(string shortUrl)
    {
        if (_cache.TryGetValue(shortUrl, out string longUrl))
        {
            return longUrl;
        }
        
        longUrl = await _db.GetLongUrlAsync(shortUrl);
        _cache.Add(longUrl, shortUrl);

        return longUrl;
    }
        

    private static string GenerateShortUrl(string longUrl)
    {
        byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(longUrl));

        var sb = new StringBuilder();
        for (int i = 0; i < ShortUrlLen; i++)
        {
            sb.Append(Chars[hash[i] % Chars.Length]);
        }

        return sb.ToString();
    }
}
