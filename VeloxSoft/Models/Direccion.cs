using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public string NumCasa { get; set; }
        public string Calle { get; set; }
        public string Cruzamientos { get; set; }
        public string Referencia { get; set; }
        public string Colonia { get; set; }

        public override string ToString() =>
            $"{NumCasa} {Calle} x {Cruzamientos}, {Colonia}";
    }
}
