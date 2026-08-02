using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_tienda_web_odi.Migrations
{
    /// <inheritdoc />
    public partial class CorregirBDtablaFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotosEnProducto");

            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "FotosProducto",
                newName: "FotoRuta");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "FotosProducto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "FotosProducto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FotosProducto_ProductoId",
                table: "FotosProducto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FotosProducto_Producto_ProductoId",
                table: "FotosProducto",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FotosProducto_Producto_ProductoId",
                table: "FotosProducto");

            migrationBuilder.DropIndex(
                name: "IX_FotosProducto_ProductoId",
                table: "FotosProducto");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "FotosProducto");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "FotosProducto");

            migrationBuilder.RenameColumn(
                name: "FotoRuta",
                table: "FotosProducto",
                newName: "Foto");

            migrationBuilder.CreateTable(
                name: "FotosEnProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FotoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosEnProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotosEnProducto_FotosProducto_FotoId",
                        column: x => x.FotoId,
                        principalTable: "FotosProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FotosEnProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FotosEnProducto_FotoId",
                table: "FotosEnProducto",
                column: "FotoId");

            migrationBuilder.CreateIndex(
                name: "IX_FotosEnProducto_ProductoId",
                table: "FotosEnProducto",
                column: "ProductoId");
        }
    }
}
