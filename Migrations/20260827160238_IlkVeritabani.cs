using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliDepo.Migrations
{
    /// <inheritdoc />
    public partial class IlkVeritabani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Barkod = table.Column<string>(type: "TEXT", nullable: true),
                    RafNo = table.Column<string>(type: "TEXT", nullable: true),
                    Adet = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdet = table.Column<int>(type: "INTEGER", nullable: false),
                    HedefAdet = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urunler");
        }
    }
}
