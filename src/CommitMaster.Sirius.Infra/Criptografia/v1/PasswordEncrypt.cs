using SecureIdentity.Password;

namespace CommitMaster.Sirius.Infra.Criptografia.v1
{
    public class PasswordEncrypt : IPasswordEncrypt
    {
        public string PasswordHash(string password)
        {
            return PasswordHasher.Hash(password);
        }
        
        public bool VerifyPassword(string hash, string password)
        {
            return PasswordHasher.Verify(hash, password);
        }
    }

    public interface IPasswordEncrypt
    {
        string PasswordHash(string password);

        bool VerifyPassword(string hash, string password);
    }
}
