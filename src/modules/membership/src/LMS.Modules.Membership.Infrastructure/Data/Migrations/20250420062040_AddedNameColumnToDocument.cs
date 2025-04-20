using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameColumnToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "membership",
                table: "documents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "membership",
                table: "documents");
        }
    }
}
