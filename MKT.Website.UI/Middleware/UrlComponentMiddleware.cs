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

            string currentCulture = Thread.CurrentThread.CurrentUICulture.Name;

            // Add a component to the URL
            url = $"/{currentCulture}" + url;

            // Update the request URL
            context.Request.Path = url;

            // Call the next middleware component
            await _next(context);
        }
    }

}
