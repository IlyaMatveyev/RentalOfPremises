using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure
{
    public interface IRentalOfPremisesDbContext
    {
        DbSet<Advert> Adverts { get; set; }
        DbSet<Premise> Premises { get; set; }
        DbSet<Response> Responses { get; set; }
        DbSet<User> Users { get; set; }
    }
}