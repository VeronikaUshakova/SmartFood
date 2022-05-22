using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartFood.DAL.Migrations
{
    public partial class InitialDbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id_box = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_product = table.Column<int>(nullable: false),
                    Initial_weight_product = table.Column<decimal>(nullable: false),
                    DateEntry_box = table.Column<DateTime>(nullable: false),
                    ShelfLife_box = table.Column<int>(nullable: false),
                    Id_delivery = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id_box);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id_delivery = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_shipper = table.Column<int>(nullable: false),
                    Id_foodEstablishment = table.Column<int>(nullable: false),
                    DateTime_delivery = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id_delivery);
                });

            migrationBuilder.CreateTable(
                name: "FoodEstablishments",
                columns: table => new
                {
                    Id_foodEstablishment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_foodEstablishment = table.Column<string>(nullable: false),
                    Address_foodEstablishment = table.Column<string>(nullable: false),
                    Mobile_foodEstablishment = table.Column<string>(nullable: false),
                    Login_foodEstablishment = table.Column<string>(nullable: false),
                    Password_foodEstablishment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEstablishments", x => x.Id_foodEstablishment);
                });

            migrationBuilder.CreateTable(
                name: "HistoryBoxes",
                columns: table => new
                {
                    Id_history = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_box = table.Column<int>(nullable: false),
                    Weight_box = table.Column<decimal>(nullable: false),
                    Temperature_box = table.Column<decimal>(nullable: false),
                    Humidity_box = table.Column<decimal>(nullable: false),
                    DateTime_history = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryBoxes", x => x.Id_history);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id_product = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_product = table.Column<string>(nullable: false),
                    Price_product = table.Column<decimal>(nullable: false),
                    Temperature_product = table.Column<decimal>(nullable: false),
                    Humidity_product = table.Column<decimal>(nullable: false),
                    Weight_product = table.Column<decimal>(nullable: false),
                    Id_shipper = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id_product);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id_shipper = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_shipper = table.Column<string>(nullable: false),
                    Address_shipper = table.Column<string>(nullable: false),
                    Mobile_shipper = table.Column<string>(nullable: false),
                    Login_shipper = table.Column<string>(nullable: false),
                    Password_shipper = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id_shipper);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "FoodEstablishments");

            migrationBuilder.DropTable(
                name: "HistoryBoxes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shippers");
        }
    }
}
