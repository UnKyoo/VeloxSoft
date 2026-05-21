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
    public partial class FormTablaDireccion : Form
    {
        public FormTablaDireccion()
        {
            InitializeComponent();
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        public void CargarDirecciones(List<Direccion> lista)
        {
            dgvDireccion.Rows.Clear();
            foreach (var dir in lista)
                dgvDireccion.Rows.Add(dir.NumCasa, dir.Calle, dir.Cruzamientos, dir.Referencia, dir.Colonia);
        }

        public DataGridView Tabla => dgvDireccion;

        public void FiltrarPorColonia(string valor)
        {
            foreach (DataGridViewRow row in dgvDireccion.Rows)
            {
                if (row.IsNewRow) continue;
                string? celda = row.Cells["Colonia"].Value?.ToString();
                row.Visible = string.IsNullOrEmpty(valor) ||
                              (celda != null && celda.IndexOf(valor,
                               StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        public void MostrarTodos()
        {
            foreach (DataGridViewRow row in dgvDireccion.Rows)
                if (!row.IsNewRow) row.Visible = true;
        }
    }
}
