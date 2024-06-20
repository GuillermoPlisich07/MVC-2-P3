namespace MVC_2_P3.DTOs
{
    public class DTOMovimientoStockCreate
    {
        public int id { get; set; }
        public List<DTOArticulo> articulos { get; set; }
        public int idArticulo { get; set; }
        public List<DTOMovimientoTipo> tipos { get; set; }
        public int idMovimientoTipo { get; set; }
        public string email { get; set; }
        public int cantidadMovidas { get; set; }
    }
}
