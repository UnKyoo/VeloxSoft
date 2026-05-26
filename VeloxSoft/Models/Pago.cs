using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class MetodoPago
    {
        public string Id { get; set; }
        public string Tipo { get; set; }

        public override string ToString() => Tipo;
    }
}