namespace VeloxSoft.Formularios
{
    partial class FormTablaProveedores
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
            dgvProveedores = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colProveedor = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            SuspendLayout();
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.AllowUserToResizeRows = false;
            dgvProveedores.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedores.BackgroundColor = Color.White;
            dgvProveedores.BorderStyle = BorderStyle.None;
            dgvProveedores.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProveedores.ColumnHeadersBorderStyle =
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
            dgvProveedores.ColumnHeadersDefaultCellStyle =
            dataGridViewCellStyle1;
            dgvProveedores.ColumnHeadersHeight = 45;
            dgvProveedores.Columns.AddRange(new DataGridViewColumn[] {
            colID,
            colProveedor
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
            dgvProveedores.DefaultCellStyle = dataGridViewCellStyle2;
            dgvProveedores.Dock = DockStyle.Fill;
            dgvProveedores.EnableHeadersVisualStyles = false;
            dgvProveedores.GridColor = Color.FromArgb(235, 235, 235);
            dgvProveedores.ReadOnly = true;
            dgvProveedores.RowHeadersVisible = false;
            dgvProveedores.RowTemplate.Height = 50;
            dgvProveedores.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.Size = new Size(700, 375);
            colID.HeaderText = "ID";
            colProveedor.HeaderText = "PROVEEDOR";
            Controls.Add(dgvProveedores);
            Name = "FormTablaProveedores";
            Text = "Lista de Proveedores";
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            ResumeLayout(false);
        }
            public DataGridView dgvProveedores;
            private DataGridViewTextBoxColumn colID;
            private DataGridViewTextBoxColumn colProveedor;
    }
}
        
