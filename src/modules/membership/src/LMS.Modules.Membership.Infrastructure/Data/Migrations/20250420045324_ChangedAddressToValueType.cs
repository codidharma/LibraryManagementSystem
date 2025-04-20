using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAddressToValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "membership");

            migrationBuilder.AddColumn<string>(
                name: "building_number",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "state",
                schema: "membership",
                table: "patrons",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street",
                schema: "membership",
                table: "patrons",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "zip_code",
                schema: "membership",
                table: "patrons",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "building_number",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "state",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "street",
                schema: "membership",
                table: "patrons");

            migrationBuilder.DropColumn(
                name: "zip_code",
                schema: "membership",
                table: "patrons");

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    building_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    city = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    patron_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    street = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_addresses_patrons_patron_id",
                        column: x => x.patron_id,
                        principalSchema: "membership",
                        principalTable: "patrons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_patron_id",
                schema: "membership",
                table: "addresses",
                column: "patron_id",
                unique: true);
        }
    }
}
