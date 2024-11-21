using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.MSSQLServer
{
    public interface IRentalOfPremisesDbContext
    {
        DbSet<AdvertEntity> Adverts { get; set; }
        DbSet<PremiseEntity> Premises { get; set; }
        DbSet<ResponseEntity> Responses { get; set; }
        DbSet<UserEntity> Users { get; set; }

        public Task SaveChangesAsync();
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}