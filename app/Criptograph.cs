using System.Security.Cryptography;

namespace Cript{
    public class PasswordHasher{
        private const int SaltSize = 8; 
        private const int HashSize = 16; 
        private const int Iterations = 100;

        public static string HashPassword(string password){
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider()){
                rng.GetBytes(salt);
            }   

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations)){
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] combinedBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, combinedBytes, 0, SaltSize);
                Array.Copy(hash, 0, combinedBytes, SaltSize, HashSize);
                return Convert.ToBase64String(combinedBytes);
            }
        }

        public static bool VerifyPassword(string hashedPassword, string enteredPassword){
            byte[] combinedBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            byte[] storedHash = new byte[HashSize];
            Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
            Array.Copy(combinedBytes, SaltSize, storedHash, 0, HashSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations)){
                byte[] enteredHash = pbkdf2.GetBytes(HashSize);

                for (int i = 0; i < HashSize; i++){
                    if (enteredHash[i] != storedHash[i]){
                        return false;
                    }
                }
            }

            return true;
        }
    }
}