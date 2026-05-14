using System;
using System.Collections.Generic;
using System.Text;

namespace VeloxSoft.Models
{
    public class DireccionItem
    {
        public int Id { get; set; }
        public string Texto { get; set; }

        public override string ToString() => Texto; // Lo que muestra el ComboBox
    }
}
