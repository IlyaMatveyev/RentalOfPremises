using Microsoft.AspNetCore.Http;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremisesService
    {
        Task<Guid> Add(Premises premises, IFormFile? mainImage);
        Task<int> Delete(Guid premisesId);
        Task<List<Premises>?> GetAll();
        Task<Premises?> GetById(Guid premisesId);
        Task<Guid> Update(Guid premisesId, Premises premises);
        Task<Guid> UpdateMainImage(Guid premisesId, IFormFile newImage);
    }
}