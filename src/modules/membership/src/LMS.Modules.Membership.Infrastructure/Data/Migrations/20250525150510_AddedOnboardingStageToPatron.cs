using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOnboardingStageToPatron : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "onboarding_stage",
                schema: "membership",
                table: "patrons",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons",
                sql: "onboarding_stage in ('PatronAdded', 'AddressAdded', 'DocumentAdded', 'DocumentsVerified')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_patrons_onboarding_stage",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "onboarding_stage",
                schema: "membership",
                table: "patrons");
        }
    }
}
