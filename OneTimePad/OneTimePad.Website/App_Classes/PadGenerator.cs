using System.Security.Cryptography;
using System.Text;

// NOTE: Pinched from (and then modified): https://eksith.wordpress.com/tag/one-time-pad
namespace OneTimePad.Website.App_Classes
{
    public class PadGenerator
    {
        // 2346789ABCDEFGHKLMNPQRTWXYZ
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <param name="a">characters per word</param>
        /// <param name="b">blocks per word</param>
        /// <param name="c">lines per block</param>
        /// <param name="d">total number of blocks</param>
        public static string Generate(int a, int b, int c, int d)
        {
            var sb = new StringBuilder();

            for (var di = 0; di < d; di++)
            {
                sb.Append($"{di + 1}.\r\n");
                for (var ci = 0; ci < c; ci++)
                {
                    for (var bi = 0; bi < b; bi++)
                    {
                        var word = GenerateRandomString(a, Alphabet);
                        sb.Append(word);

                        var isEndOfLine = bi != b - 1;
                        sb.Append(isEndOfLine ? "   " : "\r\n");
                    }
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        // Generates a random string of given length
        private static string GenerateRandomString(int len, string range)
        {
            var bytes = new byte[len];
            var chars = new char[len];

            // Shuffle the range first
            range = Shuffle(range);

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);

                for (var i = 0; i < len; i++)
                {
                    chars[i] = range[bytes[i] % range.Length];
                }
            }

            return new string(chars);
        }

        // Implements the Fisher-Yates algorithm to shuffle the range
        private static string Shuffle(string range)
        {
            var chars = range.ToCharArray();
            var len = chars.Length;
            var bytes = new byte[len];

            using (var rng = new RNGCryptoServiceProvider())
            {
                for (var i = len - 1; i > 1; i--)
                {
                    // New set of random bytes
                    rng.GetBytes(bytes);

                    var r = bytes[i] % len;
                    var c = chars[i];
                    chars[i] = chars[r]; // Swap
                    chars[r] = c;
                }
            }

            return new string(chars);
        }
    }
}