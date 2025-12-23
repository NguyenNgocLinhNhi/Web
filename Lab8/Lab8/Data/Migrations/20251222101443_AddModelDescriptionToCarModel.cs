using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab8.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddModelDescriptionToCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModelDescription",
                table: "CarModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelDescription",
                table: "CarModels");
        }
    }
}
