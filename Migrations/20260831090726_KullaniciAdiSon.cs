using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliDepo.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciAdiSon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KullaniciAdi",
                table: "Hareketler",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KullaniciAdi",
                table: "Hareketler");
        }
    }
}
