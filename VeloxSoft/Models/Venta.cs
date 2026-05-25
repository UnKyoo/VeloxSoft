using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class Venta
    {
        public long IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public DateOnly Fecha { get; set; }
        public long NumCel { get; set; }
        public string TipoDePago { get; set; }
        public string Estado { get; set; }
        public long? IdCorte { get; set; } 
        public long IdUsuario { get; set; }

    }
}
