namespace MVC_2_P3.DTOs
{
    public class DTOListadoArticuloTipoDescendente
    {
        public List<DTOArticulo> articulos {  get; set; }
        public List<DTOMovimientoTipo> tipos {  get; set; }
        public List<DTOMovimientoStock> movimientos { get; set; }
        public int idArticulo { get; set; }
        public int idTipo { get; set;}
        public int pagina { get; set; }

    }
}
