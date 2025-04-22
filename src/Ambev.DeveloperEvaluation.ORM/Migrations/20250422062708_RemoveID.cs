using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class RemoveID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Product_ProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sale_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleProducts_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SaleProducts");

            migrationBuilder.RenameTable(
                name: "SaleProducts",
                newName: "SaleProduct");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProducts_ProductId",
                table: "SaleProduct",
                newName: "IX_SaleProduct_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "SaleProduct",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "SaleProduct",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "SaleProduct",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleProduct",
                table: "SaleProduct",
                columns: new[] { "SaleId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Product_ProductId",
                table: "SaleProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProduct_Sale_SaleId",
                table: "SaleProduct",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Product_ProductId",
                table: "SaleProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProduct_Sale_SaleId",
                table: "SaleProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleProduct",
                table: "SaleProduct");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "SaleProduct");

            migrationBuilder.RenameTable(
                name: "SaleProduct",
                newName: "SaleProducts");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProduct_ProductId",
                table: "SaleProducts",
                newName: "IX_SaleProducts_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "SaleProducts",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "SaleProducts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SaleProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleProducts",
                table: "SaleProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_SaleId",
                table: "SaleProducts",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Product_ProductId",
                table: "SaleProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sale_SaleId",
                table: "SaleProducts",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
