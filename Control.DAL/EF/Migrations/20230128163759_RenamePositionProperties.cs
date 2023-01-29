using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RenamePositionProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Positions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Positions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Positions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Positions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
