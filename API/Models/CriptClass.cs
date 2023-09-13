using System.Security.Cryptography;

namespace Cript
{
    public class PasswordHasher
    {
        private const int SaltSize = 8;
        private const int HashSize = 16;
        private const int Iterations = 100;

        public static (string HashedPassword, string Salt) HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] combinedBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, combinedBytes, 0, SaltSize);
                Array.Copy(hash, 0, combinedBytes, SaltSize, HashSize);
                string hashedPassword = Convert.ToBase64String(combinedBytes);
                string saltString = Convert.ToBase64String(salt);
                return (hashedPassword, saltString);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] combinedBytes = new byte[SaltSize + HashSize];
                Array.Copy(saltBytes, 0, combinedBytes, 0, SaltSize);
                Array.Copy(hash, 0, combinedBytes, SaltSize, HashSize);
                string hashedPassword = Convert.ToBase64String(combinedBytes);
                for (int i = 0; i < (HashSize + SaltSize); i++)
                {
                    if (hashedPassword[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
