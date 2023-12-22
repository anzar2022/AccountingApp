using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApi.Migrations
{
    /// <inheritdoc />
    public partial class twoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestEMIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrincipalAmount = table.Column<double>(type: "float", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    InterestAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestEMIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestEMIs_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestEMIs_TransactionId",
                table: "InterestEMIs",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestEMIs");

            migrationBuilder.DropTable(
                name: "Interests");
        }
    }
}
