using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Detalle_Venta
    {
        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? IdServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }


        //Objetos
        public Venta Venta { get; set; }
        public Producto? Producto { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
