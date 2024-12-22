using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Infrastructure.Entities;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class ImagesInAdvertsConfiguration : IEntityTypeConfiguration<ImageInAdvertEntity>
    {
        public void Configure(EntityTypeBuilder<ImageInAdvertEntity> builder)
        {
            builder.HasKey(k => new { k.AdvertId, k.ImageUrl });

            builder
                .HasOne(x => x.AdvertEntity)
                .WithMany(x => x.ListImageUrl)
                .HasForeignKey(x => x.AdvertId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(x => x.ImageUrl)
                .IsRequired();
        }
    }
}
