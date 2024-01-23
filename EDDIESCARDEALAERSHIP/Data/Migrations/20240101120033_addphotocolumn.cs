using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDDIESCARDEALAERSHIP.Data.Migrations
{
    /// <inheritdoc />
    public partial class addphotocolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Cars");
        }
    }
}
