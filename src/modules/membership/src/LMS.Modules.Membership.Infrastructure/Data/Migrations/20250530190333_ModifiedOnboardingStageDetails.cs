using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedOnboardingStageDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons");

            migrationBuilder.AlterColumn<string>(
                name: "onboarding_stage",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AddCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons",
                sql: "onboarding_stage in ('PatronAdded', 'AddressAdded', 'DocumentAdded', 'DocumentsVerified', 'Completed')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons");

            migrationBuilder.AlterColumn<string>(
                name: "onboarding_stage",
                schema: "membership",
                table: "patrons",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons",
                sql: "onboarding_stage in ('PatronAdded', 'AddressAdded', 'DocumentAdded', 'DocumentsVerified')");
        }
    }
}
