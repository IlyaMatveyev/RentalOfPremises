using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(n => n.UserName).IsRequired().HasMaxLength(16);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(256);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(256);

            //связь с помещениями (во владении)
            builder.HasMany(u => u.PersonalPremises)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            //связь с помещениями (которые снимает user)
            builder.HasMany(u => u.RentedPremises)
                .WithOne(p => p.Renter)
                .HasForeignKey(p => p.RenterId);

            //связь с объявлениями юзера
            builder.HasMany(u => u.Adverts)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId);

            //связь с откликами (которые совершил юзер)
            builder.HasMany(u => u.Responses)
                .WithOne(r => r.Sender)
                .HasForeignKey(r => r.SenderId);

        }
    }
}
