using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientInfoData.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInitDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayInfos",
                columns: table => new
                {
                    ApartId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Housenumber = table.Column<string>(type: "TEXT", nullable: false),
                    Apartnumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayInfos", x => x.ApartId);
                });

            migrationBuilder.CreateTable(
                name: "ClientInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApartId = table.Column<int>(type: "INTEGER", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Midname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PayInfoApartId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientInfos_PayInfos_PayInfoApartId",
                        column: x => x.PayInfoApartId,
                        principalTable: "PayInfos",
                        principalColumn: "ApartId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_PayInfoApartId",
                table: "ClientInfos",
                column: "PayInfoApartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientInfos");

            migrationBuilder.DropTable(
                name: "PayInfos");
        }
    }
}
