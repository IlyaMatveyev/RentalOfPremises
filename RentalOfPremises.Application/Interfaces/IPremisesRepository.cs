using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremisesRepository
    {
        Task<Guid> Add(Premises premises);
        Task<int> Delete(Guid premisesId);
        Task<List<Premises>?> ReadAll();
        Task<Premises> ReadById(Guid premisesId);
        Task<Guid> Update(Guid premisesId, Premises premises);
        Task<Guid> UpdateMainImage(Guid premisesId, string newImageUrl);
    }
}