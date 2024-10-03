using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.EntityConfigurations;

namespace RentalOfPremises.Infrastructure
{
    public class RentalOfPremisesDbContext : DbContext, IRentalOfPremisesDbContext
    {
        public RentalOfPremisesDbContext(DbContextOptions<RentalOfPremisesDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Premise> Premises { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Advert> Adverts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PremiseConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new ResponseConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
