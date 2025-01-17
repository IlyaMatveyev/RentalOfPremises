namespace RentalOfPremises.Application.Interfaces
{
    public interface IImagesInAdvertRepository
    {
        Task<Guid> Add(Guid advertId, string imageUrl);
        Task<int> Delete(Guid advertId, string imageUrl);

    }
}
