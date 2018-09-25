using Microsoft.EntityFrameworkCore.Migrations;

namespace JjOnlineStore.Data.EF.Migrations
{
    public partial class TransportationTypeInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportationType",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationType",
                table: "Orders");
        }
    }
}
