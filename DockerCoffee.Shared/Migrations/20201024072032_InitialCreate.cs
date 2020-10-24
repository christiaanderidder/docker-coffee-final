using Microsoft.EntityFrameworkCore.Migrations;

namespace DockerCoffee.Shared.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PreparationTime = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    BeverageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "Id", "Name", "PreparationTime", "Stock" },
                values: new object[] { 1, "Espresso", 30, 10 });

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "Id", "Name", "PreparationTime", "Stock" },
                values: new object[] { 2, "Cappuccino", 60, 2 });

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "Id", "Name", "PreparationTime", "Stock" },
                values: new object[] { 3, "Americano", 45, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BeverageId",
                table: "Orders",
                column: "BeverageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Beverages");
        }
    }
}
