using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_tienda_web_odi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoPerfilUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioNotificadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImagenIcono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuario_UsuarioNotificadoId",
                        column: x => x.UsuarioNotificadoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoTransaccion = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<double>(type: "float", nullable: false),
                    Longitud = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Usuario_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompradorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NombreProductoSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenProductoSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoTransaccionProductoSnapshot = table.Column<int>(type: "int", nullable: false),
                    VisibleParaComprador = table.Column<bool>(type: "bit", nullable: false),
                    VisibleParaVendedor = table.Column<bool>(type: "bit", nullable: false),
                    PrecioProductoSnapshot = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_Usuario_CompradorId",
                        column: x => x.CompradorId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FotosProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FotoRuta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FotosProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensajeChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emisor = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajeChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensajeChat_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosMensaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MensajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosMensaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosMensaje_MensajeChat_MensajeId",
                        column: x => x.MensajeId,
                        principalTable: "MensajeChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosMensaje_MensajeId",
                table: "ArchivosMensaje",
                column: "MensajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_CompradorId",
                table: "Chat",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ProductoId",
                table: "Chat",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FotosProducto_ProductoId",
                table: "FotosProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajeChat_ChatId",
                table: "MensajeChat",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioNotificadoId",
                table: "Notificaciones",
                column: "UsuarioNotificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaId",
                table: "Producto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_VendedorId",
                table: "Producto",
                column: "VendedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosMensaje");

            migrationBuilder.DropTable(
                name: "FotosProducto");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "MensajeChat");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
