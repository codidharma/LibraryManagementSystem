using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Modules.Membership.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToAccomodateNullabilityOfDocumentsAndAccessId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_patrons_patron_id",
                schema: "membership",
                table: "documents");

            migrationBuilder.DropIndex(
                name: "IX_patrons_access_id",
                schema: "membership",
                table: "patrons");

            migrationBuilder.AlterColumn<Guid>(
                name: "access_id",
                schema: "membership",
                table: "patrons",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "patron_id",
                schema: "membership",
                table: "documents",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_patrons_patron_id",
                schema: "membership",
                table: "documents",
                column: "patron_id",
                principalSchema: "membership",
                principalTable: "patrons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_patrons_patron_id",
                schema: "membership",
                table: "documents");

            migrationBuilder.AlterColumn<Guid>(
                name: "access_id",
                schema: "membership",
                table: "patrons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "patron_id",
                schema: "membership",
                table: "documents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_patrons_access_id",
                schema: "membership",
                table: "patrons",
                column: "access_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_patrons_patron_id",
                schema: "membership",
                table: "documents",
                column: "patron_id",
                principalSchema: "membership",
                principalTable: "patrons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
