using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Perods_PeriodId",
                table: "Positions");

            migrationBuilder.RenameTable(
                name: "Perods",
                newName: "Periods");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Periods_PeriodId",
                table: "Positions",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Periods_PeriodId",
                table: "Positions");

            migrationBuilder.RenameTable(
                name: "Periods",
                newName: "Perods");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Perods_PeriodId",
                table: "Positions",
                column: "PeriodId",
                principalTable: "Perods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
