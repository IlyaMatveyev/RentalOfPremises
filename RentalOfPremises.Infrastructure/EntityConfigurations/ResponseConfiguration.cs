using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class ResponseConfiguration : IEntityTypeConfiguration<ResponseEntity>
    {
        public void Configure(EntityTypeBuilder<ResponseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Message).HasMaxLength(1024);

            //отправители
            builder.HasOne(r => r.Sender)
                .WithMany(s => s.Responses)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.NoAction); //если Sender удалится, то БД не будет производить никаких действий над откликами

            //объявление
            builder.HasOne(r => r.Advert)
                .WithMany(a => a.Responses)
                .HasForeignKey(r => r.AdvertId);
                //.OnDelete(DeleteBehavior.Cascade); //если объявление удалят, то все отклики на него удалятся
        }
    }
}
