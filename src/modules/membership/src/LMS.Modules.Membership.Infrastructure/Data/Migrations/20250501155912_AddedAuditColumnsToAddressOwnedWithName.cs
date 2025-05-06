using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditColumnsToAddressOwnedWithName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_modified_on",
                schema: "membership",
                table: "patrons",
                newName: "Patron_modified_on");

            migrationBuilder.RenameColumn(
                name: "Address_created_on",
                schema: "membership",
                table: "patrons",
                newName: "Patron_created_on");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patron_modified_on",
                schema: "membership",
                table: "patrons",
                newName: "Address_modified_on");

            migrationBuilder.RenameColumn(
                name: "Patron_created_on",
                schema: "membership",
                table: "patrons",
                newName: "Address_created_on");
        }
    }
}
