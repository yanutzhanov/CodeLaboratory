using System.Security.Cryptography;
using System.Text;

namespace CodeLaboratory.Helpers
{
    public static class MD5Algorithm
    {
        public static string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);

            StringBuilder hash = new StringBuilder();
            foreach (byte b in byteHash)
                hash.Append(string.Format("{0:x2}", b));

            return hash.ToString();
        }
    }
}
