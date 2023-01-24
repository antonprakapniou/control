using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PositionId",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PeriodId",
                table: "Periods");

            migrationBuilder.DropPrimaryKey(
                name: "OwnerId",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "OperationId",
                table: "Operations");

            migrationBuilder.DropPrimaryKey(
                name: "NominationId",
                table: "Nominations");

            migrationBuilder.DropPrimaryKey(
                name: "MeasuringId",
                table: "Measurings");

            migrationBuilder.DropPrimaryKey(
                name: "CategoryId",
                table: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Periods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Operations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Nominations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Measurings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Periods");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Operations");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Nominations");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Measurings");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PositionId",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PeriodId",
                table: "Periods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "OwnerId",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "OperationId",
                table: "Operations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "NominationId",
                table: "Nominations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "MeasuringId",
                table: "Measurings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "CategoryId",
                table: "Categories",
                column: "Id");
        }
    }
}
