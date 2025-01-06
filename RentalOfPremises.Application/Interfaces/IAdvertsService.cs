using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Application.DTOs.Pagination;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IAdvertsService
    {
        Task<Guid> Create(Advert advert);

        Task<AdvertFullInfoResponse> GetById_ForOwner(Guid advertId);
        Task<AdvertFullInfoResponse> GetById(Guid advertId);

        Task<PaginatedResult<AdvertShortInfoResponse>> GetAll_ForOwner(PaginationParams paginationParams, Guid userId);
        Task<PaginatedResult<AdvertShortInfoResponse>> GetAll(PaginationParams paginationParams);

        Task<Guid> UpdateInfo(AdvertUpdateInfoRequest advertRequest, Guid advertId);

        Task<int> Delete(Guid advertId);

        Task<Guid> PublishUnpublish(Guid advertId);



        Task<Guid> UploadMainImage(IFormFile mainImage, Guid advertId);
        Task<Guid> UpdateMainImage(IFormFile newMainImage, Guid advertId);
        Task<Guid> DeleteMainImage(Guid advertId);

    }
}
