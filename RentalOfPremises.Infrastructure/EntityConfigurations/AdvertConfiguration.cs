﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalOfPremises.Domain.Models;

namespace RentalOfPremises.Infrastructure.EntityConfigurations
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Label).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(2048);



            //отклики
            builder.HasMany(a => a.Responses)
                .WithOne(r => r.Advert)
                .HasForeignKey(r => r.AdvertId)
                .OnDelete(DeleteBehavior.Cascade); //удаление Advert вызывает удаление Responses

            //владелец
            builder.HasOne(a => a.Owner)
                .WithMany(r => r.Adverts)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); //если владелец удалён, то его Adverts тоже удаляются

            //помещение (эта связь уже прописана )
            /*builder.HasOne(a => a.Premise)
                .WithOne(p => p.Advert)
                .HasForeignKey<Advert>(a => a.PremiseId);*/
        }
    }
}