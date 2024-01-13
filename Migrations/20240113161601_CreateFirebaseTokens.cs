using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neighbor_chef.Migrations
{
    /// <inheritdoc />
    public partial class CreateFirebaseTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirebaseTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirebaseTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirebaseTokenPerson",
                columns: table => new
                {
                    FirebaseTokensId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeopleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirebaseTokenPerson", x => new { x.FirebaseTokensId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_FirebaseTokenPerson_FirebaseTokens_FirebaseTokensId",
                        column: x => x.FirebaseTokensId,
                        principalTable: "FirebaseTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirebaseTokenPerson_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirebaseTokenPerson_PeopleId",
                table: "FirebaseTokenPerson",
                column: "PeopleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirebaseTokenPerson");

            migrationBuilder.DropTable(
                name: "FirebaseTokens");
        }
    }
}
