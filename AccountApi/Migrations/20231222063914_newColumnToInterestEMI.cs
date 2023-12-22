using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApi.Migrations
{
    /// <inheritdoc />
    public partial class newColumnToInterestEMI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BalanceInterestAmount",
                table: "InterestEMIs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "GeneratedDate",
                table: "InterestEMIs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<double>(
                name: "PaidInterestAmount",
                table: "InterestEMIs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceInterestAmount",
                table: "InterestEMIs");

            migrationBuilder.DropColumn(
                name: "GeneratedDate",
                table: "InterestEMIs");

            migrationBuilder.DropColumn(
                name: "PaidInterestAmount",
                table: "InterestEMIs");
        }
    }
}
