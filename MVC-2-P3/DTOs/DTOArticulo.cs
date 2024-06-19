using System.ComponentModel;

namespace MVC_2_P3.DTOs
{
    public class DTOArticulo
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        [DisplayName("Codigo Proveedor")]
        public long codigoProveedor { get; set; }

        [DisplayName("Precio Publico")]
        public decimal precioPublico { get; set; }

        public int stock { get; set; }
    }
}
