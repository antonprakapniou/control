using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesToOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shop",
                table: "Owners",
                newName: "ShortShop");

            migrationBuilder.RenameColumn(
                name: "Production",
                table: "Owners",
                newName: "ShortProduction");

            migrationBuilder.AddColumn<string>(
                name: "FullProduction",
                table: "Owners",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullShop",
                table: "Owners",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullProduction",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "FullShop",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "ShortShop",
                table: "Owners",
                newName: "Shop");

            migrationBuilder.RenameColumn(
                name: "ShortProduction",
                table: "Owners",
                newName: "Production");
        }
    }
}
