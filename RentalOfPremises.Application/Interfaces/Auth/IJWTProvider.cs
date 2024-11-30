using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces.Auth
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}
