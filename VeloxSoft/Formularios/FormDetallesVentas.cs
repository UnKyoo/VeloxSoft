using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VeloxSoft.Models;

namespace VeloxSoft.Formularios
{ 
        public partial class FormDetallesVentas : Form
        {
            public FormDetallesVentas(string idVenta, string cliente, string fecha)
            {
                InitializeComponent();
                this.Text = $"Detalle de venta — {cliente} ({fecha})";
                
                dgvDetalles.Dock = DockStyle.Fill;
                pnlDetalles.Dock = DockStyle.Fill;
                CargarDatosPrueba(idVenta);
            }

            private void CargarDatosPrueba(string idVenta)
            {
                dgvDetalles.Rows.Add("001", "Laptop Dell", "$10,500", "1", "$10,500");
                dgvDetalles.Rows.Add("002", "Mouse inalámb.", "$350", "2", "$700");
                dgvDetalles.Rows.Add("003", "Mochila", "$580", "1", "$580");
            }
        }


    
}
