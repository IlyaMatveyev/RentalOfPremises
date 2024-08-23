using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Message).HasMaxLength(1024);

            //отправители
            builder.HasOne(r => r.Sender)
                .WithMany(s => s.Responses);

            //объявление
            builder.HasOne(r => r.Advert)
                .WithMany(a => a.Responses);
        }
    }
}
