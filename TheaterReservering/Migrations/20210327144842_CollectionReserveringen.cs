using Microsoft.EntityFrameworkCore.Migrations;

namespace TheaterReservering.Migrations
{
    public partial class CollectionReserveringen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: false),
                    Woonplaats = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserveringen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: false),
                    KlantId = table.Column<int>(nullable: true),
                    Bezet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserveringen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserveringen_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserveringen_KlantId",
                table: "Reserveringen",
                column: "KlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserveringen");

            migrationBuilder.DropTable(
                name: "Klanten");
        }
    }
}
