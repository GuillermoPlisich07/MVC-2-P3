namespace MVC_2_P3.DTOs
{
    public class DTOMovimientoStockPost
    {
        public int id { get; set; }
        public int idArticulo { get; set; }
        public int idMovimientoTipo { get; set; }
        public string email { get; set; }
        public int cantidadMovidas { get; set; }
    }
}
