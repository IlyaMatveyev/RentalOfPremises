using RentalOfPremises.Infrastructure.MSSQLServer;

namespace RentalOfPremises.Infrastructure.Repositories
{
    public class ImageInAdvertRepository
    {
        private readonly IRentalOfPremisesDbContext _dbContext;
        public ImageInAdvertRepository(IRentalOfPremisesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public Task<Guid> Add()
        {

        }*/
    }
}
