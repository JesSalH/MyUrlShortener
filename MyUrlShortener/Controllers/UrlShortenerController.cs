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
            return Json(_urlShortenerService.ShortenUrl(url));
        }

        [HttpGet, Route("/{code}")]
        public IActionResult RedirectToUrl([FromRoute] string code)
        {
            try
            {
                return Redirect(_urlShortenerService.GetOriginalUrl(code));
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/RedirectError.cshtml", new RedirectErrorPageModel() { Message = ex.Message });
            }
        }      
    }
}