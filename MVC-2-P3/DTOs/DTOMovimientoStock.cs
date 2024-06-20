namespace MVC_2_P3.DTOs
{
    public class DTOMovimientoStock
    {
        public int id { get; set; }
        public DateTime fechaDeMovimiento { get; set; }
        public DTOArticulo articulo { get; set; }
        public DTOMovimientoTipo tipo { get; set; }
        public string email { get; set; }
        public int cantidadMovidas { get; set; }
    }
}
