using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using MyUrlShortener.Models;
using MyUrlShortener.Services.UrlShortener;

namespace MyUrlShortener.Controllers
{
    public class UrlShortenerController : Controller
    {
        private readonly IUrlShortenerService _urlShortenerService;

        public UrlShortenerController(IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        [HttpPost, Route("/")]
        public IActionResult PostURL([FromBody] string url)
        {
            EnsureArg.IsNotNullOrEmpty(url, nameof(url));

            try
            {
                var shortUrl = _urlShortenerService.ShortenUrl(url);
                return Json(new JsonResponse() { Result = "OK", Code = shortUrl });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse() { Result = "Fail", Message = ex.Message });
            }
        }

        [HttpGet, Route("/{code}")]
        public IActionResult RedirectToUrl([FromRoute] string code)
        {
            try
            {
                var originalUrl = _urlShortenerService.GetOriginalUrl(code);
                return Redirect(originalUrl);
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorPageModel() { Message = ex.Message});
            }
        }      
    }
}