using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Migrations
{
    public partial class Bookinge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    packageId = table.Column<int>(type: "int", nullable: false),
                    AddTravelerCount = table.Column<int>(type: "int", nullable: true),
                    BookingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
