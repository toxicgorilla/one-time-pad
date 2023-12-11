using System;
using Microsoft.AspNetCore.Mvc;
using OneTimePad.Website.App_Classes;
using OneTimePad.Website.Models.Generate;

namespace OneTimePad.Website.Controllers
{
    public class GenerateController : Controller
    {
        private const int DefaultA = 5;

        private const int MaxA = 20;

        private const int DefaultB = 5;

        private const int MaxB = 20;

        private const int DefaultC = 10;

        private const int MaxC = 20;

        private const int DefaultD = 20;

        private const int MaxD = 200;

        /// <param name="a">characters per word</param>
        /// <param name="b">blocks per word</param>
        /// <param name="c">lines per block</param>
        /// <param name="d">total number of blocks</param>
        public ActionResult Text(int a = DefaultA, int b = DefaultB, int c = DefaultC, int d = DefaultD)
        {
            // Sanitize inputs
            a = (a > 0 && a <= MaxA) ? a : DefaultA;
            b = (b > 0 && b <= MaxB) ? b : DefaultB;
            c = (c > 0 && c <= MaxC) ? c : DefaultC;
            d = (d > 0 && d <= MaxD) ? d : DefaultD;

            // Generate
            var pad = PadGenerator.Generate(a, b, c, d);

            var viewModel = new TextViewModel { Pad = pad };

            return View(viewModel);
        }

        /// <param name="a">characters per word</param>
        /// <param name="b">blocks per word</param>
        /// <param name="c">lines per block</param>
        /// <param name="d">total number of blocks</param>
        public ActionResult Image(int a = DefaultA, int b = DefaultB, int c = DefaultC, int d = DefaultD)
        {
            // Sanitize inputs
            a = (a > 0 && a <= MaxA) ? a : DefaultA;
            b = (b > 0 && b <= MaxB) ? b : DefaultB;
            c = (c > 0 && c <= MaxC) ? c : DefaultC;
            d = (d > 0 && d <= MaxD) ? d : DefaultD;

            // Generate
            var pad = PadGenerator.Generate(a, b, c, d);
            var imageData = Convert.ToBase64String(ImageUtils.GetImage(pad));

            var viewModel = new ImageViewModel { ImageData = imageData };

            return View(viewModel);
        }
    }
}
