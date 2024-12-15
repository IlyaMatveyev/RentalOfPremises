using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalOfPremises.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_MainImageUrl_Field_in_Premises_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Premises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Premises");
        }
    }
}
