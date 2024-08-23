using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class PremiseConfiguration : IEntityTypeConfiguration<Premise>
    {
        public void Configure(EntityTypeBuilder<Premise> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Address).IsRequired().HasMaxLength(256);
            builder.Property(p => p.CoutOfRooms).IsRequired();
            builder.Property(p => p.Area).IsRequired();

            //связь с владельцем
            builder.HasOne(p => p.Owner)
                .WithMany(u => u.PersonalPremises);

            //связь с арендатором
            builder.HasOne(p => p.Renter)
                .WithMany(u => u.RentedPremises);

            //связь с объявлением
            builder.HasOne(p => p.Advert)
                .WithOne(a => a.Premise);


        }
    }
}
