using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientInfoData.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForeignKeyApartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInfos_PayInfos_PayInfoApartId",
                table: "ClientInfos");

            migrationBuilder.DropIndex(
                name: "IX_ClientInfos_PayInfoApartId",
                table: "ClientInfos");

            migrationBuilder.DropColumn(
                name: "PayInfoApartId",
                table: "ClientInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_ApartId",
                table: "ClientInfos",
                column: "ApartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInfos_PayInfos_ApartId",
                table: "ClientInfos",
                column: "ApartId",
                principalTable: "PayInfos",
                principalColumn: "ApartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInfos_PayInfos_ApartId",
                table: "ClientInfos");

            migrationBuilder.DropIndex(
                name: "IX_ClientInfos_ApartId",
                table: "ClientInfos");

            migrationBuilder.AddColumn<int>(
                name: "PayInfoApartId",
                table: "ClientInfos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfos_PayInfoApartId",
                table: "ClientInfos",
                column: "PayInfoApartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInfos_PayInfos_PayInfoApartId",
                table: "ClientInfos",
                column: "PayInfoApartId",
                principalTable: "PayInfos",
                principalColumn: "ApartId");
        }
    }
}
