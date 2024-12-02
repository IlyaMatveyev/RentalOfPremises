using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalOfPremises.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNameToPremiseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Premises",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Premises");
        }
    }
}
