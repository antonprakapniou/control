using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeToOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "Owners",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "Owners");
        }
    }
}
