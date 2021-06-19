using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreApi.Migrations
{
    public partial class skillSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 1, 30, "Faith" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 2, 40, "Honesty" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 3, 50, "Knowledge" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
