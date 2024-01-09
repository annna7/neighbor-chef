using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neighbor_chef.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToOrderMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderMeals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderMeals");
        }
    }
}
