namespace RentalOfPremises.Application.Interfaces
{
    public interface IImagesInAdvertRepository
    {
        Task<Guid> Add(Guid advertId, string imageUrl);
        Task<int> Delete(string imageUrl);

    }
}
