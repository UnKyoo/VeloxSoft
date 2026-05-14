using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Apodo { get; set; }
        public string Direccion { get; set; } // Texto concatenado desde la BD
    }
}
