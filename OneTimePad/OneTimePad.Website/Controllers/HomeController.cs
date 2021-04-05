using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using OneTimePad.Website.App_Classes;

namespace OneTimePad.Website.Controllers
{
    public class HomeController : Controller
    {
        // NOTE: Pinched from: https://eksith.wordpress.com/tag/one-time-pad/
        public ActionResult Index(IFormCollection collection)
        {
            // Generate
            var chars = "2346789ABCDEFGHKLMNPQRTWXYZ";

            // Default values
            var ds = 8;
            var dl = 882;

            // Store (s = segment size, l = number of segments)
            var s = 0;
            var l = 0;

            // Assign values
            if (int.TryParse(collection["s"], out s) || int.TryParse(collection["l"], out l))
            {
                // Set defaults to practical limits
                s = (s > 0 && s <= 20) ? s : ds;
                l = (l > 0 && l <= 1000) ? l : dl; }
            else
            {
                // Set defaults
                s = ds;
                l = dl;
            }

            var model = new PadModel();
            var txt = model.RenderPad(s, l, chars);
            var img = Convert.ToBase64String(model.GetImg(txt));

            //ViewData["pad"] = txt; // Plain text version of the pad

            // Instead of sending binary data, I opted to use a base64 encoded image instead
            // since most modern browsers support it anyway
            ViewData["img"] = $"data:image/png;base64,{img}";

            return View();
        }
    }
}
