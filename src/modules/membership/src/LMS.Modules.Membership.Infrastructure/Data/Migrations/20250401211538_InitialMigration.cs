using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "membership");

            migrationBuilder.CreateTable(
                name: "patrons",
                schema: "membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    patron_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    access_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patrons", x => x.id);
                    table.CheckConstraint("ck_patrons_patron_type", "patron_type in ('Regular', 'Research')");
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    street = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    city = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    patron_id = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "documents",
                schema: "membership",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: false),
                    patron_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.id);
                    table.CheckConstraint("ck_document_content_type", "content_type IN ('application/pdf', 'application/jpg', 'application/jpeg')");
                    table.CheckConstraint("ck_document_document_type", "document_type IN ('PersonalId', 'AcademicsId','AddressProof')");
                    table.ForeignKey(
                        name: "FK_documents_patrons_patron_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_documents_patron_id",
                schema: "membership",
                table: "documents",
                column: "patron_id");

            migrationBuilder.CreateIndex(
                name: "IX_patrons_access_id",
                schema: "membership",
                table: "patrons",
                column: "access_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_patrons_email",
                schema: "membership",
                table: "patrons",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "membership");

            migrationBuilder.DropTable(
                name: "documents",
                schema: "membership");

            migrationBuilder.DropTable(
                name: "patrons",
                schema: "membership");
        }
    }
}
