using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalOfPremises.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_IsPublished_field_in_AdvertTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Adverts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Adverts");
        }
    }
}
