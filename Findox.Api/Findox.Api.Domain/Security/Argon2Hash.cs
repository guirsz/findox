using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Findox.Api.Domain.Security
{
    /// <summary>
    /// Documentation: https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html
    /// </summary>
    public static class Argon2Hash
    {
        public static byte[] CreateSalt()
        {
            var buffer = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(buffer);
            return buffer;
        }

        public static byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 1; // one cores
            argon2.Iterations = 2;
            argon2.MemorySize = 1024 * 19; // 19MB

            return argon2.GetBytes(16);
        }

        public static bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
    }
}
