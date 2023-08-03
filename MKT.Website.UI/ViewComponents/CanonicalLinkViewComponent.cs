using Microsoft.AspNetCore.Mvc;
using MKT.Website.UI.ViewComponents;


namespace MKT.Website.UI.ViewComponents
{
    public class CanonicalLinkViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string canonicalUrl)
        {
            ViewBag.CanonicalUrl = canonicalUrl;
            return View();
        }
    }
}
