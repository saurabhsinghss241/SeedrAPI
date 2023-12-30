using BCrypt.Net;

namespace HashingService
{
    public class BCryptService : IHashingService
    {
        const int iterations = 11;
        readonly HashType hashAlgorithm = HashType.SHA512;
        public string Hash(string key)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(key, iterations, hashAlgorithm);
        }
        public bool IsValidHash(string key, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(key, hash, hashAlgorithm);
        }
    }
}
