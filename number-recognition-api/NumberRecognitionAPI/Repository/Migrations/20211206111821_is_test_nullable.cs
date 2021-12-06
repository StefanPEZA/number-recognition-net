using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class is_test_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_test",
                table: "Dataset",
                type: "INT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_test",
                table: "Dataset",
                type: "INT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
