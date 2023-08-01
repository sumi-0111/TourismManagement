using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourismApp.Migrations
{
    public partial class Sumi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgents",
                columns: table => new
                {
                    TravelAgentId = table.Column<int>(type: "int", nullable: false),
                    TravelAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelAgentEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgentPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelAgentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgents", x => x.TravelAgentId);
                    table.ForeignKey(
                        name: "FK_TravelAgents_Users_TravelAgentId",
                        column: x => x.TravelAgentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travellers",
                columns: table => new
                {
                    TravellerId = table.Column<int>(type: "int", nullable: false),
                    TravellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravellerPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravellerAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travellers", x => x.TravellerId);
                    table.ForeignKey(
                        name: "FK_Travellers_Users_TravellerId",
                        column: x => x.TravellerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelAgents");

            migrationBuilder.DropTable(
                name: "Travellers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
