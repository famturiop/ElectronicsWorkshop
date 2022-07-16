using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicsWorkshop.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CompositeDeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompositeDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BasisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompositeDevices_BaseDevices_BasisId",
                        column: x => x.BasisId,
                        principalTable: "BaseDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompositeDeviceConnector",
                columns: table => new
                {
                    CompositeDevicesId = table.Column<int>(type: "int", nullable: false),
                    ConnectorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeDeviceConnector", x => new { x.CompositeDevicesId, x.ConnectorsId });
                    table.ForeignKey(
                        name: "FK_CompositeDeviceConnector_CompositeDevices_CompositeDevicesId",
                        column: x => x.CompositeDevicesId,
                        principalTable: "CompositeDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompositeDeviceConnector_Connectors_ConnectorsId",
                        column: x => x.ConnectorsId,
                        principalTable: "Connectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompositeDeviceConnector_ConnectorsId",
                table: "CompositeDeviceConnector",
                column: "ConnectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositeDevices_BasisId",
                table: "CompositeDevices",
                column: "BasisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositeDeviceConnector");

            migrationBuilder.DropTable(
                name: "CompositeDevices");

            migrationBuilder.DropTable(
                name: "Connectors");

            migrationBuilder.DropTable(
                name: "BaseDevices");
        }
    }
}
