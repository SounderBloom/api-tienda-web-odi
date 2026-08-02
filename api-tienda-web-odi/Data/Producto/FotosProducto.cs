namespace api_tienda_web_odi.Data.Producto
{
    public class FotosProducto
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public List<FotosEnProducto> FotosEnProducto { get; set; } = new();
    }
}
