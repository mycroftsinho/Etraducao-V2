using System.Security.Cryptography;

namespace etraducao.Models.Configuration
{
    public static class BucketName
    {
        public static string RandomName()
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                string legalChars = "abcdefhijklmnpqrstuvwxyz";
                byte[] randomByte = new byte[1];
                var randomChars = new char[20];
                int nextChar = 0;
                while (nextChar < randomChars.Length)
                {
                    rng.GetBytes(randomByte);
                    if (legalChars.Contains((char)randomByte[0]))
                        randomChars[nextChar++] = (char)randomByte[0];
                }
                return new string(randomChars);
            }
        }
    }
}
