using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neighbor_chef.Migrations
{
    /// <inheritdoc />
    public partial class AddChefsAndCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvanceNoticeDays",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvailableDatesJson",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxOrdersPerDay",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId",
                table: "Meals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meals_ChefId",
                table: "Meals",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_People_ChefId",
                table: "Meals",
                column: "ChefId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_People_ChefId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_ChefId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "AdvanceNoticeDays",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AvailableDatesJson",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "People");

            migrationBuilder.DropColumn(
                name: "MaxOrdersPerDay",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Meals");
        }
    }
}
