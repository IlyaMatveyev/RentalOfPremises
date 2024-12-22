using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalOfPremises.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_PriceField_and_images_for_Advert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Adverts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Adverts",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ImagesInAdverts",
                columns: table => new
                {
                    AdvertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesInAdverts", x => new { x.AdvertId, x.ImageUrl });
                    table.ForeignKey(
                        name: "FK_ImagesInAdverts_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesInAdverts");

            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Adverts");
        }
    }
}
