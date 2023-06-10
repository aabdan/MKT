using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MKT.Website.UI.Middleware
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _shortUrl;
        private readonly string _fullUrl;

        public RedirectMiddleware(RequestDelegate next, string shortUrl, string fullUrl)
        {
            _next = next;
            _shortUrl = shortUrl;
            _fullUrl = fullUrl;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Host.Value == _shortUrl)
            {
                context.Response.Redirect(_fullUrl + context.Request.Path + context.Request.QueryString, true);
                return;
            }

            await _next(context);
        }
    }
}


