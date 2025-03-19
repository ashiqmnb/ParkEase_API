using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Address_table_updated_PostalCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Postal_code",
                table: "Addresses",
                newName: "PostalCode");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationInDays",
                table: "Companies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpiryDate",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionStartDate",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionDurationInDays",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpiryDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SubscriptionStartDate",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Addresses",
                newName: "Postal_code");
        }
    }
}
