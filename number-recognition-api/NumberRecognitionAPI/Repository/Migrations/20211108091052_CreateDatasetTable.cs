using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class CreateDatasetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dataset",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    label = table.Column<int>(type: "INT", nullable: false),
                    image = table.Column<byte[]>(type: "BLOB", nullable: true),
                    is_test = table.Column<bool>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_datasetid", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dataset");
        }
    }
}
