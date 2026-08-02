namespace api_tienda_web_odi.Data.Producto
{
    public class FotosEnProducto
    {
        public int Id { get; set; }
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int FotoId { get; set; }
        public FotosProducto Foto { get; set; }
    }
}
