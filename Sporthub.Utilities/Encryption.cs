using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace Sporthub.Utilities
{
    public class Encryption
    {
        public static string EncryptEmail(string email)
        {
            try
            {
                SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                byte[] hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(email));
                StringBuilder digest = new StringBuilder();
                foreach (byte n in hash) digest.Append(Convert.ToInt32(n + 256).ToString("x2"));

                return digest.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}
