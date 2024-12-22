using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Application.Interfaces
{
    public interface IAdvertsRepository
    {
        Task<Guid> Add(Advert advert);
        Task<Advert> ReadById(Guid advertId, Guid? userId = null);
    }
}
