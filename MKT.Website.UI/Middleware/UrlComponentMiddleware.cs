using Azure;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MKT.Website.UI.Middleware
{
    public class UrlComponentMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlComponentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the current URL
            var url = context.Request.Path;
            if ( url != null)
            {
                string[] urlSegments = url.Value.Split('/');
                if (urlSegments.Length > 1 && (urlSegments[1].Contains("ar-AE") || urlSegments[1].Contains("ar") || urlSegments[1].Contains("en-US")
                    || urlSegments[1].Contains("en") || urlSegments[1].Contains("fr-FR") || urlSegments[1].Contains("fr")) ) {
                    //context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    //CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(urlSegments[1])),
                    //new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                    var culture = new CultureInfo(urlSegments[1] == "en"? "en-US": urlSegments[1] == "ar"? "ar-AE": "fr-FR");

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
            }
            

            //string currentCulture = Thread.CurrentThread.CurrentUICulture.Name;

            //// Add a component to the URL
            //url = $"/{currentCulture}" + url;

            //// Update the request URL
            //context.Request.Path = url;

            // Call the next middleware component
            await _next(context);
        }
    }

}
