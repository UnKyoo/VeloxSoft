namespace VeloxSoft.Formularios
{
    partial class FormTablaDireccion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvDireccion = new DataGridView();
            colNumeroCasa = new DataGridViewTextBoxColumn();
            colCalle = new DataGridViewTextBoxColumn();
            colCruzamientos = new DataGridViewTextBoxColumn();
            colReferencia = new DataGridViewTextBoxColumn();
            colColonia = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvDireccion).BeginInit();
            SuspendLayout();
            // 
            // dgvDireccion
            // 
            dgvDireccion.AllowUserToAddRows = false;
            dgvDireccion.AllowUserToResizeRows = false;
            dgvDireccion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDireccion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDireccion.BackgroundColor = Color.White;
            dgvDireccion.BorderStyle = BorderStyle.None;
            dgvDireccion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDireccion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDireccion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDireccion.ColumnHeadersHeight = 45;
            dgvDireccion.Columns.AddRange(new DataGridViewColumn[] { colNumeroCasa, colCalle, colCruzamientos, colReferencia, colColonia });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvDireccion.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDireccion.Dock = DockStyle.Fill;
            dgvDireccion.EnableHeadersVisualStyles = false;
            dgvDireccion.GridColor = Color.FromArgb(235, 235, 235);
            dgvDireccion.Location = new Point(0, 0);
            dgvDireccion.Margin = new Padding(3, 2, 3, 2);
            dgvDireccion.Name = "dgvDireccion";
            dgvDireccion.ReadOnly = true;
            dgvDireccion.RowHeadersVisible = false;
            dgvDireccion.RowTemplate.Height = 50;
            dgvDireccion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDireccion.Size = new Size(788, 375);
            dgvDireccion.TabIndex = 0;
            // 
            // colNumeroCasa
            // 
            colNumeroCasa.FillWeight = 50F;
            colNumeroCasa.HeaderText = "N° CASA";
            colNumeroCasa.Name = "colNumeroCasa";
            colNumeroCasa.ReadOnly = true;
            // 
            // colCalle
            // 
            colCalle.HeaderText = "CALLE";
            colCalle.Name = "colCalle";
            colCalle.ReadOnly = true;
            // 
            // colCruzamientos
            // 
            colCruzamientos.FillWeight = 120F;
            colCruzamientos.HeaderText = "CRUZAMIENTOS";
            colCruzamientos.Name = "colCruzamientos";
            colCruzamientos.ReadOnly = true;
            // 
            // colReferencia
            // 
            colReferencia.FillWeight = 150F;
            colReferencia.HeaderText = "REFERENCIA";
            colReferencia.Name = "colReferencia";
            colReferencia.ReadOnly = true;
            // 
            // colColonia
            // 
            colColonia.HeaderText = "COLONIA";
            colColonia.Name = "colColonia";
            colColonia.ReadOnly = true;
            // 
            // FormTablaDireccion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(788, 375);
            Controls.Add(dgvDireccion);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormTablaDireccion";
            Text = "Gestión de Direcciones";
            ((System.ComponentModel.ISupportInitialize)dgvDireccion).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvDireccion;
        private DataGridViewTextBoxColumn colNumeroCasa;
        private DataGridViewTextBoxColumn colCalle;
        private DataGridViewTextBoxColumn colCruzamientos;
        private DataGridViewTextBoxColumn colReferencia;
        private DataGridViewTextBoxColumn colColonia;
    }
}