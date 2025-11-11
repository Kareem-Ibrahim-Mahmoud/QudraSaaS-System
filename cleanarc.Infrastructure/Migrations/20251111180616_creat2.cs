using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QudraSaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class creat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "CarModel");

            migrationBuilder.AddColumn<int>(
                name: "NextChange",
                table: "ServiceSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ServiceSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "discountPercentage",
                table: "Ranks",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whats",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "oTPCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTPHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oTPCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "serviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    available = table.Column<bool>(type: "bit", nullable: false),
                    workShopId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serviceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_serviceTypes_AspNetUsers_workShopId",
                        column: x => x.workShopId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_serviceTypes_workShopId",
                table: "serviceTypes",
                column: "workShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oTPCodes");

            migrationBuilder.DropTable(
                name: "serviceTypes");

            migrationBuilder.DropColumn(
                name: "NextChange",
                table: "ServiceSessions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ServiceSessions");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "whats",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CarModel",
                table: "Cars",
                newName: "Model");

            migrationBuilder.AlterColumn<double>(
                name: "discountPercentage",
                table: "Ranks",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
