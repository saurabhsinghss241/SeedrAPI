using BCrypt.Net;

namespace HashingService
{
    public class BCryptService : IHashingService
    {
        const int iterations = 11;
        HashType hashAlgorithm = HashType.SHA512;
        public string Encrypt(string key)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(key, iterations, hashAlgorithm);
        }
        public bool Decrypt(string key, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(key, hash, hashAlgorithm);
        }
    }
}
