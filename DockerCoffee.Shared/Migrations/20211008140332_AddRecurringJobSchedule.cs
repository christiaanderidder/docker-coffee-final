using Microsoft.EntityFrameworkCore.Migrations;

namespace DockerCoffee.Shared.Migrations
{
    public partial class AddRecurringJobSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringJobSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringJobSchedules", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RecurringJobSchedules",
                columns: new[] { "Id", "CronExpression", "Type" },
                values: new object[] { 1, "*/10 * * * * ?", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringJobSchedules");
        }
    }
}
