namespace MVC_2_P3.DTOs
{
    public class DTOListadoArticuloRangoPorFecha
    {
        public List<DTOMovimientoStock> movimientos {  get; set; }
        public List<DTOArticulo> articulos { get; set; }
        public List<int> idArticulos { get; set; }
        public DateTime inicio { get; set; }
        public DateTime final {  get; set; }
        public int pagina { get; set; }

    }
}
