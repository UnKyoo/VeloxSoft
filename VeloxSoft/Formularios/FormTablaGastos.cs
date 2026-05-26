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
    public partial class FormTablaGastos : Form
    {
        private readonly ServicioGasto _ServicioGasto;

        public FormTablaGastos(ServicioGasto servicioGasto)
        {
            InitializeComponent();
            _ServicioGasto = servicioGasto;
        }

        public void CargarDatos(string fechaInicio, string fechaFin,string proveedor)
        {
            MessageBox.Show("CargarDatos ejecutado"); // ← agrega esto
            var lista = _ServicioGasto.Buscar_DetalleGastos(fechaInicio, fechaFin, proveedor, out string error);


            MessageBox.Show($"Error: '{error}'\nRegistros: {lista.Count}\nFecha inicio: {fechaInicio}\nFecha fin: {fechaFin}");

            if (!string.IsNullOrEmpty(error)) { MessageBox.Show(error); return; }

            dgvGastos.Rows.Clear();
            foreach (var d in lista)
            {
                dgvGastos.Rows.Add(
                    d.IdGastoD,
                    $"${d.Monto.ToString("F2")}",
                    d.Descripcion,
                    d.Fecha
                );
            }
        }
    }
}
