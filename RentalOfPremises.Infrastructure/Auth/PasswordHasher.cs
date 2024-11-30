using RentalOfPremises.Application.Interfaces.Auth;

namespace RentalOfPremises.Infrastructure.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, HashedPassword);
        }
    }
}
