using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Infrastructure.Entities;
using RentalOfPremises.Infrastructure.EntityConfigurations;

namespace RentalOfPremises.Infrastructure.MSSQLServer
{
    public class RentalOfPremisesDbContext : DbContext, IRentalOfPremisesDbContext
    {
        public RentalOfPremisesDbContext(DbContextOptions<RentalOfPremisesDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PremiseEntity> Premises { get; set; }
        public DbSet<AdvertEntity> Adverts { get; set; }
        public DbSet<ImageInAdvertEntity> ImagesInAdverts { get; set; }
        public DbSet<ResponseEntity> Responses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PremisesConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new ResponseConfiguration());
            modelBuilder.ApplyConfiguration(new ImagesInAdvertsConfiguration());

            base.OnModelCreating(modelBuilder);
        }


        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
        }
    }
}
