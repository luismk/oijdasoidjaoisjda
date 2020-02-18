using System.Security.Cryptography;
using System.Text;

namespace PangyaAPI.Tools
{
    public static class MD5
    {
        /// <summary>
        /// Criptografa string em MD5 Hash
        /// </summary>
        public static string StringToMd5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
