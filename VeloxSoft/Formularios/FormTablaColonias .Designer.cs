namespace VeloxSoft.Formularios
{
    partial class FormTablaColonias
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
            dgvColonias = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colColonia = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvColonias).BeginInit();
            SuspendLayout();
            // 
            // dgvColonias
            // 
            dgvColonias.AllowUserToAddRows = false;
            dgvColonias.AllowUserToResizeRows = false;
            dgvColonias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvColonias.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvColonias.BackgroundColor = Color.White;
            dgvColonias.BorderStyle = BorderStyle.None;
            dgvColonias.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvColonias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvColonias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvColonias.ColumnHeadersHeight = 45;
            dgvColonias.Columns.AddRange(new DataGridViewColumn[] { colID, colColonia });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvColonias.DefaultCellStyle = dataGridViewCellStyle2;
            dgvColonias.Dock = DockStyle.Fill;
            dgvColonias.EnableHeadersVisualStyles = false;
            dgvColonias.GridColor = Color.FromArgb(235, 235, 235);
            dgvColonias.Location = new Point(0, 0);
            dgvColonias.Margin = new Padding(3, 2, 3, 2);
            dgvColonias.Name = "dgvColonias";
            dgvColonias.ReadOnly = true;
            dgvColonias.RowHeadersVisible = false;
            dgvColonias.RowTemplate.Height = 50;
            dgvColonias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvColonias.Size = new Size(700, 375);
            dgvColonias.TabIndex = 0;
            // 
            // colID
            // 
            colID.FillWeight = 40F;
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colColonia
            // 
            colColonia.FillWeight = 160F;
            colColonia.HeaderText = "COLONIA";
            colColonia.Name = "colColonia";
            colColonia.ReadOnly = true;
            // 
            // FormTablaColonias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 375);
            Controls.Add(dgvColonias);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormTablaColonias";
            Text = "Gestión de Colonias";
            ((System.ComponentModel.ISupportInitialize)dgvColonias).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvColonias;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colColonia;
    }
}