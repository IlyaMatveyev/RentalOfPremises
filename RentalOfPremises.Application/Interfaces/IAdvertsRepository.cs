using RentalOfPremises.Application.DTOs.Pagination;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IAdvertsRepository
    {
        Task<Guid> Add(Advert advert);
        Task<PaginatedResult<Advert>> ReadAll(PaginationParams paginationParams, Guid? userId = null);
        Task<Advert> ReadById(Guid advertId, Guid? userId = null);

        Task<Guid> UpdateInfo(Advert advert, Guid advertId);

        Task<int> Delete(Guid advertId);

        Task<Guid> PublishUnpublish(Guid advertId);

        Task<Guid> UpdateMainImage(string mainImageUrl, Guid advertId);
    }
}
