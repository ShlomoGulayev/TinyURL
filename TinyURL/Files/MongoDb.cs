using MongoDB.Driver;

namespace TinyURL.Files;

public class MongoDb
{
    private readonly IMongoCollection<Url> _urlMap;

    public MongoDb(IMongoDatabase db)
    {
        _urlMap = db.GetCollection<Url>("Urls");
    }

    public async Task<string> GetLongUrlAsync(string shortUrl)
    {
        var urlMap = await _urlMap.Find(m => m.ShortUrl == shortUrl).FirstOrDefaultAsync();
        return urlMap.LongUrl;
    }

    public async Task<string> GetShortUrlAsync(string shortUrl)
    {
        var urlMap = await _urlMap.Find(m => m.LongUrl == shortUrl).FirstOrDefaultAsync();
        return urlMap.ShortUrl;
    }

    public async Task AddToMapAsync(Url map)
    {
        await _urlMap.InsertOneAsync(map);
    }
}
