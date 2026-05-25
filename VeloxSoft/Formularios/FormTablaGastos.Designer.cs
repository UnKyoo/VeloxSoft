namespace VeloxSoft.Formularios
{
    partial class FormTablaGastos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {

            DataGridViewCellStyle dataGridViewCellStyle1 = new
            DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new
            DataGridViewCellStyle();
            dgvGastos = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            colProveedor = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvGastos).BeginInit();
            SuspendLayout();
            dgvGastos.AllowUserToAddRows = false;
            dgvGastos.AllowUserToResizeRows = false;
            dgvGastos.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
            dgvGastos.BackgroundColor = Color.White;
            dgvGastos.BorderStyle = BorderStyle.None;
            dgvGastos.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;
            dgvGastos.ColumnHeadersBorderStyle =
            DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment =
            DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F,
            FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(234,
            243, 222);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvGastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvGastos.ColumnHeadersHeight = 45;
            dgvGastos.Columns.AddRange(new DataGridViewColumn[] {
            colID,
            colMonto,
            colProveedor,
            colFecha
            });
            dataGridViewCellStyle2.Alignment =
            DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(245,
            245, 245);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(80,
            80, 80);
            dgvGastos.DefaultCellStyle = dataGridViewCellStyle2;
            dgvGastos.Dock = DockStyle.Fill;
            dgvGastos.EnableHeadersVisualStyles = false;
            dgvGastos.GridColor = Color.FromArgb(235, 235, 235);
            dgvGastos.ReadOnly = true;
            dgvGastos.RowHeadersVisible = false;
            dgvGastos.RowTemplate.Height = 50;
            dgvGastos.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
            dgvGastos.Size = new Size(700, 375);
            colID.HeaderText = "ID";
            colMonto.HeaderText = "MONTO";
            colProveedor.HeaderText = "PROVEEDOR";
            colFecha.HeaderText = "FECHA";
            Controls.Add(dgvGastos);
            Name = "FormTablaGastos";
            Text = "Lista de Gastos";
            ((System.ComponentModel.ISupportInitialize)dgvGastos).EndInit();
            ResumeLayout(false);
        }
            public DataGridView dgvGastos;
            private DataGridViewTextBoxColumn colID;
            private DataGridViewTextBoxColumn colMonto;
            private DataGridViewTextBoxColumn colProveedor;
            private DataGridViewTextBoxColumn colFecha;


    }
}