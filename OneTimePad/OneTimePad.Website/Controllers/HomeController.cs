using System;

using Microsoft.AspNetCore.Mvc;
using OneTimePad.Website.App_Classes;

namespace OneTimePad.Website.Controllers
{
    // NOTE: Pinched from: https://eksith.wordpress.com/tag/one-time-pad
    public class HomeController : Controller
    {
        private const int DefaultS = 8;

        private const int MaxS = 20;

        private const int DefaultL = 882;

        private const int MaxL = 1000;

        // s = segment size, l = number of segments
        public ActionResult Index(int s = 0, int l = 0)
        {
            // Generate
            var chars = "2346789ABCDEFGHKLMNPQRTWXYZ";

            // Set defaults to practical limits
            s = (s > 0 && s <= MaxS) ? s : DefaultS;
            l = (l > 0 && l <= MaxL) ? l : DefaultL;

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
