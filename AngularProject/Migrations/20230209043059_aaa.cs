using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularProject.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dept2",
                columns: table => new
                {
                    deptid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    deptname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dept2", x => x.deptid);
                });

            migrationBuilder.CreateTable(
                name: "items2",
                columns: table => new
                {
                    itemcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    itemname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deptid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items2", x => x.itemcode);
                    table.ForeignKey(
                        name: "FK_items2_dept2_deptid",
                        column: x => x.deptid,
                        principalTable: "dept2",
                        principalColumn: "deptid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items2_deptid",
                table: "items2",
                column: "deptid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items2");

            migrationBuilder.DropTable(
                name: "dept2");
        }
    }
}
