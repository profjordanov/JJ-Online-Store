using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JjOnlineStore.Data.EF.Migrations
{
    public partial class CartItemOnDeleteReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItemsOnDeleteReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    Quantity = table.Column<short>(nullable: false),
                    CartId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemsOnDeleteReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemsOnDeleteReport_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItemsOnDeleteReport_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemsOnDeleteReport_CartId",
                table: "CartItemsOnDeleteReport",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemsOnDeleteReport_ProductId",
                table: "CartItemsOnDeleteReport",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemsOnDeleteReport");
        }
    }
}
