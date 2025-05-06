using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditColumnsToAddressOwnedWithUpdatedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Patron_modified_on",
                schema: "membership",
                table: "patrons",
                newName: "address.modified_on");

            migrationBuilder.RenameColumn(
                name: "Patron_created_on",
                schema: "membership",
                table: "patrons",
                newName: "address.created_on");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "address.modified_on",
                schema: "membership",
                table: "patrons",
                newName: "Patron_modified_on");

            migrationBuilder.RenameColumn(
                name: "address.created_on",
                schema: "membership",
                table: "patrons",
                newName: "Patron_created_on");
        }
    }
}
