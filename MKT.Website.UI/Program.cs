using MKT.Website.Data;
using MKT.Website.Services;
using MKT.Website.UI.Sitemap;
using MKT.Website.UI.Models;
using MKT.Infrastructure.AzureBlobStorage;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

using System.Text.Json.Serialization;
using System.Configuration;
using System.Reflection;
using System.Globalization;
using MKT.Website.UI.Middleware;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
// Add services to the container.
#region Localization
//Step 1
builder.Services.AddSingleton<LanguageService>();
// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// Configure supported cultures
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
            {
                           new CultureInfo("ar-AE"),
                           new CultureInfo("en-US"),
                           new CultureInfo("fr-FR")

            };

        options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.SetDefaultCulture("ar-AE");


        //options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());


    });

#endregion


// Add services for sitemap
#region sitemap
builder.Services.Configure<XmlSitemapOptions>(builder.Configuration.GetSection("XmlSitemap"));
builder.Services.AddScoped<IXmlSitemapGenerator, XmlSitemapGenerator>();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
#endregion


#region Identity
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<UserManager<ApplicationUser>>();

#endregion


// Storage
builder.Services.AddSingleton<IAzureBlobStorageConfiguration>(new AzureBlobStorageConfiguration
{
    ConnectionString = builder.Configuration.GetConnectionString("AzureBlobStorage"),
});

// Add services to Enable compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable compression for HTTPS requests
});

// Response Caching Middleware
builder.Services.AddResponseCaching();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");

}
else
{
    DeveloperExceptionPageOptions developerExceptionPageoptions = new DeveloperExceptionPageOptions
    {
        SourceCodeLineCount = 1
    };
    app.UseDeveloperExceptionPage(developerExceptionPageoptions);
}

// Enable localization middleware
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();


app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    //Google Page Speed- Serve static assets with an efficient cache policy
    OnPrepareResponse = _ =>
    {
        _.Context.Response.Headers.Append("Cache-Control", string.Format("public,max-age={0}", TimeSpan.FromHours(12).TotalSeconds));

    }
});

app.UseResponseCompression();

//app.MapGet("/sitemap.xml", async (IXmlSitemapGenerator sitemapGenerator) =>
//{
//    var sitemapStream = sitemapGenerator.GenerateSitemap();

//    await using (var responseStream = sitemapStream)
//    {
//        var response = sitemapStream.ToArray();
//        await responseStream.WriteAsync(response);
//    }
//});



app.UseRouting();
//app.UseMiddleware<RedirectMiddleware>("technexus.com", "https://www.technexus.com");
app.UseMiddleware<UrlComponentMiddleware>();
app.UseRewriter(new RewriteOptions()
            .AddRedirectToNonWwwPermanent()
            .AddRedirectToHttps()
         );


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "sitemap",
        pattern: "sitemap.xml",
        defaults: new { controller = "Sitemap", action = "Index" });
    endpoints.MapRazorPages();
});

app.MapControllerRoute(
    "lang",
    "{lang}/{controller}/{action}/{id?}", new { controller = "Home", action = "Main", lang = "en" });

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action}/{id?}");

app.UseResponseCaching();


app.Run();
