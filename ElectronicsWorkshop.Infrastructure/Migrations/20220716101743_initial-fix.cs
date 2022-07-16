using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicsWorkshop.Infrastructure.Migrations
{
    public partial class initialfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompositeDeviceId",
                table: "Connectors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompositeDeviceId",
                table: "Connectors",
                type: "int",
                nullable: true);
        }
    }
}
