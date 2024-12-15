using Microsoft.AspNetCore.Http;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IPremisesService
    {
        Task<Guid> Add(Premise premise, Guid userId);
        Task<int> Delete(Guid premisId, Guid userId);
        Task<List<Premise>?> GetAll(Guid userId);
        Task<Premise?> GetById(Guid premisId, Guid userId);
        Task<Guid> Update(Guid premisId, Premise premise, Guid userId);
        Task<Guid> UpdateMainImage(Guid premisesId, IFormFile newImage, Guid userId);
    }
}