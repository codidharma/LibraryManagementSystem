using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddeNationalIdColumnToPatron : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "national_id",
                schema: "membership",
                table: "patrons",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_patrons_national_id",
                schema: "membership",
                table: "patrons",
                column: "national_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_patrons_national_id",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "national_id",
                schema: "membership",
                table: "patrons");
        }
    }
}
