using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguracionMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogProduction",
                columns: table => new
                {
                    CatalogProduction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogProduction_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CatalogProduction_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogProduction_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CatalogProduction_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProduction", x => x.CatalogProduction_id);
                });

            migrationBuilder.CreateTable(
                name: "KG_Catalog",
                columns: table => new
                {
                    KGCatalog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KGCatalog_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KGCatalog_cantidad = table.Column<double>(type: "float", nullable: false),
                    KGCatalog_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KGCatalog_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KG_Catalog", x => x.KGCatalog_id);
                });

            migrationBuilder.CreateTable(
                name: "Price_KG",
                columns: table => new
                {
                    PriceKG_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceKG_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceKG_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceKG_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceKG_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price_KG", x => x.PriceKG_id);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterial",
                columns: table => new
                {
                    RawMateria_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawMaterial_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RawMaterial_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RawMaterial_KgTotal = table.Column<double>(type: "float", nullable: false),
                    RawMaterial_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RawMaterial_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterial", x => x.RawMateria_id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Rol_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rol_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol_status = table.Column<bool>(type: "bit", nullable: false),
                    Rol_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rol_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Rol_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_CI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_cel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_id);
                });

            migrationBuilder.CreateTable(
                name: "ImageCatalogProduction",
                columns: table => new
                {
                    ImageCatalogProduction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageCatalogProduction_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageCatalogProduction_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageCatalogProduction_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageCatalogProduction_updted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageCatalogProduction_IDCatalogProduction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageCatalogProduction", x => x.ImageCatalogProduction_id);
                    table.ForeignKey(
                        name: "FK_ImageCatalogProduction_CatalogProduction_ImageCatalogProduction_IDCatalogProduction",
                        column: x => x.ImageCatalogProduction_IDCatalogProduction,
                        principalTable: "CatalogProduction",
                        principalColumn: "CatalogProduction_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataCatalogProduct",
                columns: table => new
                {
                    DataCatalogProduct_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCatalogProduct_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCatalogProduct_countTotal = table.Column<int>(type: "int", nullable: false),
                    DataCatalogProduct_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCatalogProduct_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCatalogProduct_IDCatalogProduct = table.Column<int>(type: "int", nullable: false),
                    DataCatalogProduct_IDKGCatalog = table.Column<int>(type: "int", nullable: false),
                    DataCatalogProduct_IDPriceKG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCatalogProduct", x => x.DataCatalogProduct_id);
                    table.ForeignKey(
                        name: "FK_DataCatalogProduct_CatalogProduction_DataCatalogProduct_IDCatalogProduct",
                        column: x => x.DataCatalogProduct_IDCatalogProduct,
                        principalTable: "CatalogProduction",
                        principalColumn: "CatalogProduction_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataCatalogProduct_KG_Catalog_DataCatalogProduct_IDKGCatalog",
                        column: x => x.DataCatalogProduct_IDKGCatalog,
                        principalTable: "KG_Catalog",
                        principalColumn: "KGCatalog_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataCatalogProduct_Price_KG_DataCatalogProduct_IDPriceKG",
                        column: x => x.DataCatalogProduct_IDPriceKG,
                        principalTable: "Price_KG",
                        principalColumn: "PriceKG_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageRawMaterial",
                columns: table => new
                {
                    ImageRawMaterial_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageRawMaterial_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageRawMaterial_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageRawMaterial_fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageRawMaterial_fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageRawMaterial_IDRawMaterial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageRawMaterial", x => x.ImageRawMaterial_id);
                    table.ForeignKey(
                        name: "FK_ImageRawMaterial_RawMaterial_ImageRawMaterial_IDRawMaterial",
                        column: x => x.ImageRawMaterial_IDRawMaterial,
                        principalTable: "RawMaterial",
                        principalColumn: "RawMateria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KgMonitoring",
                columns: table => new
                {
                    KgMonitoring_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KgMonitoring_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KgMonitoring_KGStandard = table.Column<double>(type: "float", nullable: false),
                    KgMonitoring_Total = table.Column<double>(type: "float", nullable: false),
                    KgMonitoring_priceUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KgMonitoring_priceTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KgMonitoring_IDRawMaterial = table.Column<int>(type: "int", nullable: false),
                    KgMonitoring_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KgMonitoring_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KgMonitoring", x => x.KgMonitoring_id);
                    table.ForeignKey(
                        name: "FK_KgMonitoring_RawMaterial_KgMonitoring_IDRawMaterial",
                        column: x => x.KgMonitoring_IDRawMaterial,
                        principalTable: "RawMaterial",
                        principalColumn: "RawMateria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Profile_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Profile_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Profile_IDUser = table.Column<int>(type: "int", nullable: false),
                    Profile_IDRol = table.Column<int>(type: "int", nullable: false),
                    Profile_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Profile_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Profile_id);
                    table.ForeignKey(
                        name: "FK_Profile_Rol_Profile_IDRol",
                        column: x => x.Profile_IDRol,
                        principalTable: "Rol",
                        principalColumn: "Rol_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profile_User_Profile_IDUser",
                        column: x => x.Profile_IDUser,
                        principalTable: "User",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Sale_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sale_count = table.Column<int>(type: "int", nullable: false),
                    Sale_priceTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sale_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sale_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sale_IDDataCatalogProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Sale_id);
                    table.ForeignKey(
                        name: "FK_Sale_DataCatalogProduct_Sale_IDDataCatalogProduct",
                        column: x => x.Sale_IDDataCatalogProduct,
                        principalTable: "DataCatalogProduct",
                        principalColumn: "DataCatalogProduct_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Production",
                columns: table => new
                {
                    Production_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Production_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Production_KGTotal = table.Column<double>(type: "float", nullable: false),
                    Production_status = table.Column<int>(type: "int", nullable: false),
                    Production_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Production_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Production_IDCatalogProduction = table.Column<int>(type: "int", nullable: false),
                    Production_IDProfile = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production", x => x.Production_id);
                    table.ForeignKey(
                        name: "FK_Production_CatalogProduction_Production_IDCatalogProduction",
                        column: x => x.Production_IDCatalogProduction,
                        principalTable: "CatalogProduction",
                        principalColumn: "CatalogProduction_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Production_Profile_Production_IDProfile",
                        column: x => x.Production_IDProfile,
                        principalTable: "Profile",
                        principalColumn: "Profile_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    Accounting_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accounting_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accounting_ingreso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Accounting_egreso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Accounting_saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Accounting_IDKgMonitoring = table.Column<int>(type: "int", nullable: true),
                    Accounting_IdSales = table.Column<int>(type: "int", nullable: true),
                    Accounting_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accounting_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Accounting_id);
                    table.ForeignKey(
                        name: "FK_Accounting_KgMonitoring_Accounting_IDKgMonitoring",
                        column: x => x.Accounting_IDKgMonitoring,
                        principalTable: "KgMonitoring",
                        principalColumn: "KgMonitoring_id");
                    table.ForeignKey(
                        name: "FK_Accounting_Sale_Accounting_IdSales",
                        column: x => x.Accounting_IdSales,
                        principalTable: "Sale",
                        principalColumn: "Sale_id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialProduction",
                columns: table => new
                {
                    MaterialProduction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialProduction_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialProduction_KGUsed = table.Column<double>(type: "float", nullable: false),
                    MaterialProduction_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialProduction_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialProduction_IDRawMaterial = table.Column<int>(type: "int", nullable: false),
                    MaterialProduction_IDProduction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialProduction", x => x.MaterialProduction_id);
                    table.ForeignKey(
                        name: "FK_MaterialProduction_Production_MaterialProduction_IDProduction",
                        column: x => x.MaterialProduction_IDProduction,
                        principalTable: "Production",
                        principalColumn: "Production_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialProduction_RawMaterial_MaterialProduction_IDRawMaterial",
                        column: x => x.MaterialProduction_IDRawMaterial,
                        principalTable: "RawMaterial",
                        principalColumn: "RawMateria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductManufactured",
                columns: table => new
                {
                    ProductManufactured_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductManufactured_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductManufactured_count = table.Column<int>(type: "int", nullable: false),
                    ProductManufactured_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductManufactured_updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductManufactured_IDDataCatalogProduct = table.Column<int>(type: "int", nullable: false),
                    ProductManufactured_IDProduction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManufactured", x => x.ProductManufactured_id);
                    table.ForeignKey(
                        name: "FK_ProductManufactured_DataCatalogProduct_ProductManufactured_IDDataCatalogProduct",
                        column: x => x.ProductManufactured_IDDataCatalogProduct,
                        principalTable: "DataCatalogProduct",
                        principalColumn: "DataCatalogProduct_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductManufactured_Production_ProductManufactured_IDProduction",
                        column: x => x.ProductManufactured_IDProduction,
                        principalTable: "Production",
                        principalColumn: "Production_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Accounting_IDKgMonitoring",
                table: "Accounting",
                column: "Accounting_IDKgMonitoring");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Accounting_IdSales",
                table: "Accounting",
                column: "Accounting_IdSales");

            migrationBuilder.CreateIndex(
                name: "IX_DataCatalogProduct_DataCatalogProduct_IDCatalogProduct",
                table: "DataCatalogProduct",
                column: "DataCatalogProduct_IDCatalogProduct");

            migrationBuilder.CreateIndex(
                name: "IX_DataCatalogProduct_DataCatalogProduct_IDKGCatalog",
                table: "DataCatalogProduct",
                column: "DataCatalogProduct_IDKGCatalog");

            migrationBuilder.CreateIndex(
                name: "IX_DataCatalogProduct_DataCatalogProduct_IDPriceKG",
                table: "DataCatalogProduct",
                column: "DataCatalogProduct_IDPriceKG");

            migrationBuilder.CreateIndex(
                name: "IX_ImageCatalogProduction_ImageCatalogProduction_IDCatalogProduction",
                table: "ImageCatalogProduction",
                column: "ImageCatalogProduction_IDCatalogProduction");

            migrationBuilder.CreateIndex(
                name: "IX_ImageRawMaterial_ImageRawMaterial_IDRawMaterial",
                table: "ImageRawMaterial",
                column: "ImageRawMaterial_IDRawMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_KgMonitoring_KgMonitoring_IDRawMaterial",
                table: "KgMonitoring",
                column: "KgMonitoring_IDRawMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialProduction_MaterialProduction_IDProduction",
                table: "MaterialProduction",
                column: "MaterialProduction_IDProduction");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialProduction_MaterialProduction_IDRawMaterial",
                table: "MaterialProduction",
                column: "MaterialProduction_IDRawMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Production_Production_IDCatalogProduction",
                table: "Production",
                column: "Production_IDCatalogProduction");

            migrationBuilder.CreateIndex(
                name: "IX_Production_Production_IDProfile",
                table: "Production",
                column: "Production_IDProfile");

            migrationBuilder.CreateIndex(
                name: "IX_ProductManufactured_ProductManufactured_IDDataCatalogProduct",
                table: "ProductManufactured",
                column: "ProductManufactured_IDDataCatalogProduct");

            migrationBuilder.CreateIndex(
                name: "IX_ProductManufactured_ProductManufactured_IDProduction",
                table: "ProductManufactured",
                column: "ProductManufactured_IDProduction");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Profile_IDRol",
                table: "Profile",
                column: "Profile_IDRol");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Profile_IDUser",
                table: "Profile",
                column: "Profile_IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Sale_IDDataCatalogProduct",
                table: "Sale",
                column: "Sale_IDDataCatalogProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting");

            migrationBuilder.DropTable(
                name: "ImageCatalogProduction");

            migrationBuilder.DropTable(
                name: "ImageRawMaterial");

            migrationBuilder.DropTable(
                name: "MaterialProduction");

            migrationBuilder.DropTable(
                name: "ProductManufactured");

            migrationBuilder.DropTable(
                name: "KgMonitoring");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Production");

            migrationBuilder.DropTable(
                name: "RawMaterial");

            migrationBuilder.DropTable(
                name: "DataCatalogProduct");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "CatalogProduction");

            migrationBuilder.DropTable(
                name: "KG_Catalog");

            migrationBuilder.DropTable(
                name: "Price_KG");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
