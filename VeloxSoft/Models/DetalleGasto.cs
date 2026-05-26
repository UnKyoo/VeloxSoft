using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class DetalleGasto
    {
        public long IdGastoD { get; set; }
        public decimal Monto { get; set; }
        public DateOnly Fecha { get; set; }
        public long IdGasto { get; set; }
        public long? IdCorte { get; set; }
        public string Descripcion { get; set; }
    }
}
