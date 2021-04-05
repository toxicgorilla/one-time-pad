// THIS SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

using System.Security.Cryptography;
using System.Text;

namespace OneTimePad.Website.App_Classes
{
    public class PadGenerator
    {
        // Creates formatted pages of keys
        public static string RenderPad(int s, int l, string alphabet)
        {
            // Result
            var sb = new StringBuilder();

            // First page
            var p = 1;

            for (var i = 0; i < l; i++)
            {
                // First page number
                if (p == 1 && i == 0)
                {
                    sb.Append("1.\n\n");
                }

                // Generate segment
                sb.Append(GenerateRandomString(s, alphabet));

                // Page, number and segment separation
                if (i % 63 == 62)
                {
                    if (i + 1 < l)
                    {
                        p++;
                        sb.Append("\n\n\n");
                        sb.Append(p);
                        sb.Append(".\n\n");
                    }
                }
                else if (i % 7 == 6) // Line separation
                {
                    sb.Append("\n");
                }
                else // Segment separation
                {
                    sb.Append("   ");
                }
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