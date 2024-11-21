using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class PremiseConfiguration : IEntityTypeConfiguration<PremiseEntity>
    {
        public void Configure(EntityTypeBuilder<PremiseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Address).IsRequired().HasMaxLength(256);
            builder.Property(p => p.CoutOfRooms).IsRequired();
            builder.Property(p => p.Area).IsRequired();


            //связь с владельцем
            builder.HasOne(p => p.Owner)
                .WithMany(u => u.PersonalPremises)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); //удаление владельца запрещено если у него есть помещения

            //связь с арендатором
            builder.HasOne(p => p.Renter)
                .WithMany(u => u.RentedPremises)
                .HasForeignKey(p => p.RenterId) 
                .OnDelete(DeleteBehavior.SetNull); //при удалении арендатора, он заменияется на null. Помещение не удаляется

            //связь с объявлением !!!!!!!!
            builder.HasOne(p => p.Advert)
                .WithOne(a => a.Premise)
                .HasForeignKey<AdvertEntity>(a => a.PremiseId) //для 1:1 это нужно прописать, не смотря на то что Premise не имеет внешнего ключа
                .OnDelete(DeleteBehavior.Cascade); //В связях 1:1 OnDelete работает по другому!!! Если удалить Premise, то удалится и Advert!!!

        }
    }
}
