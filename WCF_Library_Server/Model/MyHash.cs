using System.Security.Cryptography;
using System.Text;

namespace WCF_Library_Server.Model
{
    public static class MyHash
    {
        public static bool Authefication(byte[] hashingPass, string inputPassword)
        {
            byte[] newHash;

            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();

            SHA256 sha256 = SHA256.Create();

            newHash = sha256.ComputeHash(unicodeEncoding.GetBytes(inputPassword));

            bool same = true;

            for (int x = 0; x < hashingPass.Length; x++)
            {
                if (newHash[x] != hashingPass[x])
                {
                    same = false;
                }
            }

            // возвращае результат
            if (same)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] HashedPassword(string passwordStr)
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();

            SHA256 sha256 = SHA256.Create();

            return sha256.ComputeHash(unicodeEncoding.GetBytes(passwordStr));
        }
    }
}
