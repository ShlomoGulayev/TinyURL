using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Mvc;

namespace TinyURL.Files;

[ApiController]
[Route("[controller]")]
public class RedirectionController : ControllerBase
{
    private readonly UrlShorteningService _urlShorteningService;

    public RedirectionController(UrlShorteningService urlShorteningService)
    {
        _urlShorteningService = urlShorteningService;
    }


    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> RedirectToLongUrl(string shortUrl)
    {
        var longUrl = await _urlShorteningService.GetLongUrlAsync(shortUrl);
        if (longUrl == null)
        {
            return NotFound();
        }
        return Redirect(longUrl);
    }

    [HttpPost]
    [Route("shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] string longUrl)
    {
        var shortUrl = await _urlShorteningService.ShortenUrlAsync(longUrl);
        return Ok(new { shortUrl });
    }
}
