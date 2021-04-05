using System;
using Microsoft.AspNetCore.Mvc;
using OneTimePad.Website.App_Classes;
using OneTimePad.Website.Models.Generate;

namespace OneTimePad.Website.Controllers
{
    // NOTE: Pinched from: https://eksith.wordpress.com/tag/one-time-pad
    public class GenerateController : Controller
    {
        // 2346789ABCDEFGHKLMNPQRTWXYZ
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private const int DefaultS = 8;

        private const int MaxS = 20;

        private const int DefaultL = 882;

        private const int MaxL = 1000;

        /// <param name="s">segment size</param>
        /// <param name="l">number of segments</param>
        public ActionResult Image(int s = 0, int l = 0)
        {
            // Sanitize input parameters
            s = (s > 0 && s <= MaxS) ? s : DefaultS;
            l = (l > 0 && l <= MaxL) ? l : DefaultL;

            // Generate
            var pad = PadGenerator.RenderPad(s, l, Alphabet);
            var imageData = Convert.ToBase64String(ImageUtils.GetImage(pad));

            //ViewData["pad"] = txt; // Plain text version of the pad

            var viewModel = new ImageViewModel { ImageData = imageData };

            return View(viewModel);
        }

        /// <param name="s">segment size</param>
        /// <param name="l">number of segments</param>
        public ActionResult Text(int s = 0, int l = 0)
        {
            // Sanitize input parameters
            s = (s > 0 && s <= MaxS) ? s : DefaultS;
            l = (l > 0 && l <= MaxL) ? l : DefaultL;

            // Generate
            var pad = PadGenerator.RenderPad(s, l, Alphabet);

            var viewModel = new TextViewModel { Pad = pad };

            return View(viewModel);
        }
    }
}
