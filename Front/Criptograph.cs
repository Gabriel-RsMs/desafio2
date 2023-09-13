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

        
        // public static bool VerifyPassword(string hashedPassword, string enteredPassword, string saltFromDatabase)
        // {
        //     byte[] combinedBytes = Convert.FromBase64String(hashedPassword);
        //     byte[] storedSalt = Convert.FromBase64String(saltFromDatabase);
        //     byte[] storedHash = new byte[HashSize];
        //     Array.Copy(combinedBytes, SaltSize, storedHash, 0, HashSize);

        //     using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, storedSalt, Iterations))
        //     {
        //         byte[] enteredHash = pbkdf2.GetBytes(HashSize);

        //         for (int i = 0; i < HashSize; i++)
        //         {
        //             if (enteredHash[i] != storedHash[i])
        //             {
        //                 return false;
        //             }
        //         }
        //     }

        //     return true;
        // }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, Iterations))
            {
                byte[] enteredHashBytes = pbkdf2.GetBytes(HashSize);

                for (int i = 0; i < HashSize; i++)
                {
                    if (enteredHashBytes[i] != storedHashBytes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
