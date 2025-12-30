using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoProductManufactured2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufactured_DataCatalogProduct_ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured");

            migrationBuilder.AlterColumn<int>(
                name: "ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufactured_DataCatalogProduct_ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured",
                column: "ProductManufactured_IDDataCatalogProduct",
                principalTable: "DataCatalogProduct",
                principalColumn: "DataCatalogProduct_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufactured_DataCatalogProduct_ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured");

            migrationBuilder.AlterColumn<int>(
                name: "ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufactured_DataCatalogProduct_ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured",
                column: "ProductManufactured_IDDataCatalogProduct",
                principalTable: "DataCatalogProduct",
                principalColumn: "DataCatalogProduct_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
