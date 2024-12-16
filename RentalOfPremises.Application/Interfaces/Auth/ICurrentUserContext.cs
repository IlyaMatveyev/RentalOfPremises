namespace RentalOfPremises.Application.Interfaces.Auth
{
    public interface ICurrentUserContext
    {
        public Guid UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
