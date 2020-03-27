using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeLaboratory.Helpers
{
    public static class MD5Algorithm
    {
        public static string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            StringBuilder hash = new StringBuilder();

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash.Append(string.Format("{0:x2}", b));

            return hash.ToString();
        }
    }
}
