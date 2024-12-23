﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalOfPremises.Infrastructure.MSSQLServer;

#nullable disable

namespace RentalOfPremises.Infrastructure.Migrations
{
    [DbContext(typeof(RentalOfPremisesDbContext))]
    partial class RentalOfPremisesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.AdvertEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("MainImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PremiseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PremiseId")
                        .IsUnique();

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.ImageInAdvertEntity", b =>
                {
                    b.Property<Guid>("AdvertId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdvertId", "ImageUrl");

                    b.ToTable("ImagesInAdverts");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.PremiseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int>("CoutOfRooms")
                        .HasColumnType("int");

                    b.Property<string>("MainImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RenterId");

                    b.ToTable("Premises");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.ResponseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdvertId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid?>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdvertId");

                    b.HasIndex("SenderId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.AdvertEntity", b =>
                {
                    b.HasOne("RentalOfPremises.Infrastructure.Entities.UserEntity", "Owner")
                        .WithMany("Adverts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalOfPremises.Infrastructure.Entities.PremiseEntity", "Premise")
                        .WithOne("Advert")
                        .HasForeignKey("RentalOfPremises.Infrastructure.Entities.AdvertEntity", "PremiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Premise");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.ImageInAdvertEntity", b =>
                {
                    b.HasOne("RentalOfPremises.Infrastructure.Entities.AdvertEntity", "AdvertEntity")
                        .WithMany("ListImageUrl")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AdvertEntity");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.PremiseEntity", b =>
                {
                    b.HasOne("RentalOfPremises.Infrastructure.Entities.UserEntity", "Owner")
                        .WithMany("PersonalPremises")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RentalOfPremises.Infrastructure.Entities.UserEntity", "Renter")
                        .WithMany("RentedPremises")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Owner");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.ResponseEntity", b =>
                {
                    b.HasOne("RentalOfPremises.Infrastructure.Entities.AdvertEntity", "Advert")
                        .WithMany("Responses")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalOfPremises.Infrastructure.Entities.UserEntity", "Sender")
                        .WithMany("Responses")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Advert");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.AdvertEntity", b =>
                {
                    b.Navigation("ListImageUrl");

                    b.Navigation("Responses");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.PremiseEntity", b =>
                {
                    b.Navigation("Advert");
                });

            modelBuilder.Entity("RentalOfPremises.Infrastructure.Entities.UserEntity", b =>
                {
                    b.Navigation("Adverts");

                    b.Navigation("PersonalPremises");

                    b.Navigation("RentedPremises");

                    b.Navigation("Responses");
                });
#pragma warning restore 612, 618
        }
    }
}
