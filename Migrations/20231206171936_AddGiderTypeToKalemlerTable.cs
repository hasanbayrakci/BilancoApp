using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilancoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGiderTypeToKalemlerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GiderType",
                table: "Kalemler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiderType",
                table: "Kalemler");
        }
    }
}
