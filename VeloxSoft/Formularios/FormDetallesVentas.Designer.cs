namespace VeloxSoft.Formularios
{
    partial class FormDetallesVentas
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pnlDetalles = new Panel();
            dgvDetalles = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colPrecioU = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colImporteT = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            lblInfo = new Label();
            pnlDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDetalles
            // 
            pnlDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlDetalles.Controls.Add(dgvDetalles);
            pnlDetalles.Location = new Point(0, 62);
            pnlDetalles.Name = "pnlDetalles";
            pnlDetalles.Size = new Size(1051, 572);
            pnlDetalles.TabIndex = 0;
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToResizeColumns = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(250, 250, 250);
            dgvDetalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetalles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvDetalles.ColumnHeadersHeight = 45;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDetalles.Columns.AddRange(new DataGridViewColumn[] { colID, colNombre, colPrecioU, colCantidad, colImporteT });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvDetalles.DefaultCellStyle = dataGridViewCellStyle6;
            dgvDetalles.Dock = DockStyle.Fill;
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.GridColor = Color.FromArgb(235, 235, 235);
            dgvDetalles.Location = new Point(0, 0);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.RowHeadersWidth = 51;
            dgvDetalles.RowTemplate.Height = 50;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.Size = new Size(1051, 572);
            dgvDetalles.TabIndex = 0;
            // 
            // colID
            // 
            colID.FillWeight = 50F;
            colID.HeaderText = "ID";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 180F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colPrecioU
            // 
            colPrecioU.FillWeight = 90F;
            colPrecioU.HeaderText = "Precio unitario";
            colPrecioU.MinimumWidth = 6;
            colPrecioU.Name = "colPrecioU";
            colPrecioU.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.FillWeight = 70F;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 6;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colImporteT
            // 
            colImporteT.HeaderText = "Importe total";
            colImporteT.MinimumWidth = 6;
            colImporteT.Name = "colImporteT";
            colImporteT.ReadOnly = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(lblInfo);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1051, 59);
            panel1.TabIndex = 1;
            // 
            // lblInfo
            // 
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.Font = new Font("Century Gothic", 12.8F);
            lblInfo.ForeColor = Color.FromArgb(59, 109, 17);
            lblInfo.Location = new Point(0, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(1051, 59);
            lblInfo.TabIndex = 59;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormDetallesVentas
            // 
            ClientSize = new Size(1051, 634);
            Controls.Add(panel1);
            Controls.Add(pnlDetalles);
            Name = "FormDetallesVentas";
            Text = "FormDetallesVenta";
            pnlDetalles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlDetalles;
        private DataGridView dgvDetalles;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colPrecioU;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colImporteT;
        private Panel panel1;
        private Label lblInfo;
    }
}