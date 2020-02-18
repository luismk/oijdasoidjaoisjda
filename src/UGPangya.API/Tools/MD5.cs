using System.Security.Cryptography;
using System.Text;

namespace UGPangya.API.Tools
{
    public static class MD5
    {
        /// <summary>
        ///     Criptografa string em MD5 Hash
        /// </summary>
        public static string StringToMd5(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (var i = 0; i < bytes.Length; i++) hash.Append(bytes[i].ToString("x2"));
            return hash.ToString();
        }
    }
}