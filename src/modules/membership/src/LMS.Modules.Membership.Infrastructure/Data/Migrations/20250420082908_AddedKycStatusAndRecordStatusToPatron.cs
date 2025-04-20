using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedKycStatusAndRecordStatusToPatron : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kyc_status",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "membership",
                table: "patrons",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "ck_patrons_kyc_status",
                schema: "membership",
                table: "patrons",
                sql: "kyc_status in ('Pending', 'InProgress', 'Completed', 'Failed')");

            migrationBuilder.AddCheckConstraint(
                name: "ck_patrons_status",
                schema: "membership",
                table: "patrons",
                sql: "status in ('Active', 'InActive')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_patrons_kyc_status",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropCheckConstraint(
                name: "ck_patrons_status",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "kyc_status",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "membership",
                table: "patrons");
        }
    }
}
