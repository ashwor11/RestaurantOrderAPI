using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class solution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tables_TableId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Orders_CurrentOrderId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_CurrentOrderId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CurrentOrderId",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_OrderId",
                table: "Tables",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tables_TableId",
                table: "Orders",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tables_TableId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Orders_OrderId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_OrderId",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "CurrentOrderId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_CurrentOrderId",
                table: "Tables",
                column: "CurrentOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tables_TableId",
                table: "Orders",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Orders_CurrentOrderId",
                table: "Tables",
                column: "CurrentOrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
