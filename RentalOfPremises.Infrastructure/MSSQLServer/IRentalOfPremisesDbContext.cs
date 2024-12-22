using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.MSSQLServer
{
    public interface IRentalOfPremisesDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<PremiseEntity> Premises { get; set; }
        DbSet<AdvertEntity> Adverts { get; set; }
        DbSet<ImageInAdvertEntity> ImagesInAdverts { get; set; }
        DbSet<ResponseEntity> Responses { get; set; }

        public Task SaveChangesAsync();
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}