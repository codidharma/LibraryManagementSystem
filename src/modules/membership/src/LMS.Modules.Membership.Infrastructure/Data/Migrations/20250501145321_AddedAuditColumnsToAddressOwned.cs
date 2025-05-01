using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditColumnsToAddressOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Address_created_on",
                schema: "membership",
                table: "patrons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Address_modified_on",
                schema: "membership",
                table: "patrons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_created_on",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "Address_modified_on",
                schema: "membership",
                table: "patrons");
        }
    }
}
