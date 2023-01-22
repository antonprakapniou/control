using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Units_UnitsId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.RenameColumn(
                name: "UnitsId",
                table: "Positions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_UnitsId",
                table: "Positions",
                newName: "IX_Positions_CategoryId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoryId", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Categories_CategoryId",
                table: "Positions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Categories_CategoryId",
                table: "Positions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Positions",
                newName: "UnitsId");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_CategoryId",
                table: "Positions",
                newName: "IX_Positions_UnitsId");

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UnitsId", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Units_UnitsId",
                table: "Positions",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
