using Microsoft.AspNetCore.Http;
using RentalOfPremises.Application.DTOs.AdvertDto;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IAdvertsService
    {
        Task<Guid> Create(Advert advert);

        Task<AdvertFullInfoResponse> GetById_ForOwner(Guid advertId);
        Task<AdvertFullInfoResponse> GetById(Guid advertId);

        Task<Guid> UpdateInfo(AdvertUpdateInfoRequest advertRequest, Guid advertId);

        Task<int> Delete(Guid advertId);

        Task<Guid> PublishUnpublish(Guid advertId);

        Task<Guid> UploadMainImage(IFormFile mainImage, Guid advertId);


    }
}
