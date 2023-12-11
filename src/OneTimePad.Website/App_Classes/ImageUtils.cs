using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace OneTimePad.Website.App_Classes
{
    public class ImageUtils
    {
        // Generates a jpeg of given text
        public static byte[] GetImage(string text)
        {
            var fontFamily = FontFamily.GenericMonospace;
            var fontStyle = FontStyle.Bold;
            var graphicsUnit = GraphicsUnit.Pixel;
            var fontSize = 10;

            // Blank image
            var bmp = new Bitmap(1, 1);
            var gfx = Graphics.FromImage(bmp);

            // Font settings
            var fnt = new Font(fontFamily, fontSize, fontStyle, graphicsUnit);

            // Image dimensions
            var w = (int)gfx.MeasureString(text, fnt).Width;
            var h = (int)gfx.MeasureString(text, fnt).Height;

            // New image to text size
            bmp = new Bitmap(bmp, new Size(w, h));

            gfx = Graphics.FromImage(bmp);

            // Defaults
            gfx.Clear(Color.White);
            gfx.SmoothingMode = SmoothingMode.Default;

            gfx.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.AntiAlias;

            gfx.DrawString(text, fnt, new SolidBrush(Color.Black), 0, 0);

            // Cleanup
            gfx.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
