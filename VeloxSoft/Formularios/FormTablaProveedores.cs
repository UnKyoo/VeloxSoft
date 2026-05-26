using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormTablaProveedores : Form
    {
        private readonly ServicioGasto _ServicioGasto;

        public FormTablaProveedores(ServicioGasto servicioGasto)
        {
            InitializeComponent();
            _ServicioGasto = servicioGasto;
        }

        public void CargarDatos()
        {
            var lista = _ServicioGasto.Buscar_Gastos(out string error);
            if (!string.IsNullOrEmpty(error)) { MessageBox.Show(error); return; }

            dgvProveedores.Rows.Clear();
            foreach (var g in lista)
            {
                dgvProveedores.Rows.Add(
                    g.IdGasto,
                    g.Descripcion
                );
            }
        }
    }
}
