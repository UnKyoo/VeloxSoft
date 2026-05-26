using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class Corte
    {
        public long IdCorte { get; set; }
        public string Fecha { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalGasto { get; set; }
        public decimal Ganancia { get; set; }
    }
}