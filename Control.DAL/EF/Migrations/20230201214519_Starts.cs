using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Control.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Starts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullShop = table.Column<string>(type: "TEXT", nullable: true),
                    ShortShop = table.Column<string>(type: "TEXT", nullable: false),
                    FullProduction = table.Column<string>(type: "TEXT", nullable: true),
                    ShortProduction = table.Column<string>(type: "TEXT", nullable: true),
                    ShopCode = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    MasterId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Included = table.Column<string>(type: "TEXT", nullable: true),
                    Addition = table.Column<string>(type: "TEXT", nullable: true),
                    PreviousDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NextDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    MeasuringId = table.Column<Guid>(type: "TEXT", nullable: true),
                    NominationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OperationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PeriodId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Positions_Measurings_MeasuringId",
                        column: x => x.MeasuringId,
                        principalTable: "Measurings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Positions_Nominations_NominationId",
                        column: x => x.NominationId,
                        principalTable: "Nominations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Positions_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Positions_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Positions_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_MasterId",
                table: "Owners",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CategoryId",
                table: "Positions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_MeasuringId",
                table: "Positions",
                column: "MeasuringId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_NominationId",
                table: "Positions",
                column: "NominationId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OperationId",
                table: "Positions",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OwnerId",
                table: "Positions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PeriodId",
                table: "Positions",
                column: "PeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Measurings");

            migrationBuilder.DropTable(
                name: "Nominations");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Masters");
        }
    }
}
