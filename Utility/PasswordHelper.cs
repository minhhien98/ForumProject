using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class PasswordHelper
    {
        public static string Sha256(string code, string username)
        {
            var sha = SHA256.Create();
            var encoded = sha.ComputeHash(Encoding.Default.GetBytes(code + CreateSalt(username)));
            return Tostring(encoded);
        }
        private static string CreateSalt(string id)
        {
            var userBytes = Encoding.ASCII.GetBytes(id);
            long xored = 0x00;

            foreach (var x in userBytes)
            {
                xored = ++xored ^ x;
            }

            var rand = new Random(Convert.ToInt32(xored));
            var salt = rand.Next().ToString(CultureInfo.InvariantCulture);
            salt += rand.Next().ToString(CultureInfo.InvariantCulture);
            salt += rand.Next().ToString(CultureInfo.InvariantCulture);
            salt += rand.Next().ToString(CultureInfo.InvariantCulture);
            return salt.Substring(salt.Length / 5, salt.Length / 2);
        }

        private static string Tostring(byte[] encoded)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encoded.Length; i++)
            {
                builder.Append(encoded[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
