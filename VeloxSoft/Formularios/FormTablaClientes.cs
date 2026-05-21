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
    public partial class FormTablaClientes : Form
    {
            public FormTablaClientes()
            {
                InitializeComponent();
                this.TopLevel = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Dock = DockStyle.Fill;
            }

        //LÓGICA (GIL)

        //Método que llama a la lista enviada desde el FormClientes para cargar los datos en el DataGridView
        public void CargarClientes(List<Cliente> lista) //recibe la lista de clientes desde el FormClientes
        {
            dgvClientes.Rows.Clear();

            foreach (var cliente in lista)
            {
                dgvClientes.Rows.Add(
                    cliente.IdCliente,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Apodo,
                    cliente.Direccion);
            }
        }
 
        //DISEÑO (ARMANDO)
        public DataGridView Tabla => dgvClientes;

            public void FiltrarPorNumero(string numero)
            {
                foreach (DataGridViewRow row in dgvClientes.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string texto = row.Cells["Numero"].Value?.ToString() ?? "";

                    row.Visible =
                        texto.IndexOf(numero, StringComparison.OrdinalIgnoreCase) >= 0;
                }
             }

        public void MostrarTodos()
            {
                foreach (DataGridViewRow row in dgvClientes.Rows)
                    if (!row.IsNewRow) row.Visible = true;
            }
        }
    }