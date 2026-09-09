using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliDepo.Migrations
{
    /// <inheritdoc />
    public partial class RfidAlanEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RfidUid",
                table: "Kullanicilar",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RfidUid",
                table: "Kullanicilar");
        }
    }
}
