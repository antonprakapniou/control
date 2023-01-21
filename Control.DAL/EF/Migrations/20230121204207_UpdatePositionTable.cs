using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePositionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "Previous",
                table: "Positions",
                newName: "PreviousDate");

            migrationBuilder.RenameColumn(
                name: "Next",
                table: "Positions",
                newName: "NextDate");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "PreviousDate",
                table: "Positions",
                newName: "Previous");

            migrationBuilder.RenameColumn(
                name: "NextDate",
                table: "Positions",
                newName: "Next");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Positions",
                type: "uuid",
                nullable: true);
        }
    }
}
