﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleTransfer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetupIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_DocumentNumber_DocumentType",
                table: "Users",
                columns: new[] { "DocumentNumber", "DocumentType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_DocumentNumber_DocumentType",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");
        }
    }
}
