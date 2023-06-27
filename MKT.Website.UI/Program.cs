using Microsoft.EntityFrameworkCore;
using MKT.Website.Data;
using MKT.Infrastructure.AzureBlobStorage;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Reflection;
using MKT.Website.Services;
using MKT.Website.UI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using MKT.Website.UI.Sitemap;
using Microsoft.AspNetCore.Identity;
using MKT.Website.UI.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
#region Localization
//Step 1
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("SharedResource", assemblyName.Name);
        };
    });

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
            {
                           new CultureInfo("ar-AE"),
                           new CultureInfo("en-US"),
                           new CultureInfo("fr-FR")

            };

        options.DefaultRequestCulture = new RequestCulture(culture: "ar-AE", uiCulture: "ar-AE");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });
#endregion


// Add services for sitemap
#region sitemap
builder.Services.Configure<XmlSitemapOptions>(builder.Configuration.GetSection("XmlSitemap"));
builder.Services.AddScoped<IXmlSitemapGenerator, XmlSitemapGenerator>();
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

// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapGet("/sitemap.xml", async (IXmlSitemapGenerator sitemapGenerator) =>
{
    var sitemapStream = sitemapGenerator.GenerateSitemap();

    await using (var responseStream = sitemapStream)
    {
        var response = sitemapStream.ToArray();
        await responseStream.WriteAsync(response);
    }
});



app.UseRouting();
//app.UseMiddleware<RedirectMiddleware>("technexus.com", "https://www.technexus.com");
app.UseRewriter(new RewriteOptions()
            .AddRedirectToNonWwwPermanent()
            .AddRedirectToHttps()
         );
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


app.UseAuthorization();



app.MapControllerRoute(
    "lang",
    "{lang}/{controller}/{action}/{id?}", new { controller = "Home", action = "Main", lang = "en-US" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");



app.Run();
