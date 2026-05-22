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
            pnlDetalles = new Panel();
            dgvDetalles = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colPrecioU = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colImporteT = new DataGridViewTextBoxColumn();
            pnlDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            SuspendLayout();
            // 
            // pnlDetalles
            // 
            pnlDetalles.Controls.Add(dgvDetalles);
            pnlDetalles.Location = new Point(0, 0);
            pnlDetalles.Name = "pnlDetalles";
            pnlDetalles.Size = new Size(1053, 638);
            pnlDetalles.TabIndex = 0;
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToResizeColumns = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.BorderStyle = BorderStyle.None;
            dgvDetalles.Dock = DockStyle.Fill;

            // --- ESTILO DE BORDES Y CUADRÍCULA ---
            dgvDetalles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetalles.GridColor = Color.FromArgb(235, 235, 235);

            // --- ENCABEZADOS (HEADERS) ---
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDetalles.ColumnHeadersHeight = 45;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Fondo verde claro y texto verde oscuro
            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 243, 222);
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(85, 125, 70);
            dgvDetalles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDetalles.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 243, 222);
            dgvDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // --- ESTILO DE FILAS Y CELDAS ---
            dgvDetalles.RowTemplate.Height = 50;

            dgvDetalles.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvDetalles.DefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);
            dgvDetalles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dgvDetalles.DefaultCellStyle.SelectionForeColor = Color.FromArgb(80, 80, 80);
            dgvDetalles.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDetalles.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // --- FILAS ALTERNADAS (opcional para look moderno) ---
            dgvDetalles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            // --- COLUMNAS ---
            dgvDetalles.Columns.AddRange(new DataGridViewColumn[]
            {
    colID, colNombre, colPrecioU, colCantidad, colImporteT
            });

            // --- CONFIGURACIÓN DE COLUMNAS ---
            colID.HeaderText = "ID";
            colID.Name = "ID";
            colID.FillWeight = 50;

            colNombre.HeaderText = "PRODUCTO";
            colNombre.Name = "Nombre";
            colNombre.FillWeight = 180;

            colPrecioU.HeaderText = "PRECIO UNITARIO";
            colPrecioU.Name = "PrecioU";
            colPrecioU.FillWeight = 90;

            colCantidad.HeaderText = "CANTIDAD";
            colCantidad.Name = "Cantidad";
            colCantidad.FillWeight = 70;

            colImporteT.HeaderText = "IMPORTE TOTAL";
            colImporteT.Name = "ImporteT";
            colImporteT.FillWeight = 100;

            // --- DATAGRIDVIEW ---
            dgvDetalles.Location = new Point(3, 3);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.RowHeadersWidth = 51;
            dgvDetalles.Size = new Size(1047, 632);
            dgvDetalles.TabIndex = 0;
            //
            // colID
            // 
            colID.HeaderText = "ID";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            // 
            // colNombre
            // 
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            // 
            // colPrecioU
            // 
            colPrecioU.HeaderText = "Precio unitario";
            colPrecioU.MinimumWidth = 6;
            colPrecioU.Name = "colPrecioU";
            // 
            // colCantidad
            // 
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 6;
            colCantidad.Name = "colCantidad";
            // 
            // colImporteT
            // 
            colImporteT.HeaderText = "Importe total";
            colImporteT.MinimumWidth = 6;
            colImporteT.Name = "colImporteT";
            // 
            // FormDetallesVentas
            // 
            ClientSize = new Size(1051, 634);
            Controls.Add(pnlDetalles);
            Name = "FormDetallesVentas";
            Text = "FormDetallesVenta";
            pnlDetalles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
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

    }
}