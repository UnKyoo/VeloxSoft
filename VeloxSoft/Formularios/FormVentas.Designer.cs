namespace VeloxSoft.Formularios
{
    partial class FormVentas
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
            pnlVentas = new Panel();
            pnlBD = new Panel();
            dgvVentas = new DataGridView();
            colVenta = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colImporte = new DataGridViewTextBoxColumn();
            colTpago = new DataGridViewTextBoxColumn();
            colCorte = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colUsuario = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            pnlBotones = new Panel();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            checkFecha = new CheckBox();
            lblNCliente = new Label();
            lblNVenta = new Label();
            textBuscarN = new TextBox();
            textBuscarU = new TextBox();
            lblNumero = new Label();
            lblUsuario = new Label();
            btnFiltrar = new FontAwesome.Sharp.IconButton();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            pnlFormulario = new Panel();
            pnlVentas.SuspendLayout();
            pnlBD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVentas
            // 
            pnlVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlVentas.BackColor = Color.FromArgb(234, 243, 222);
            pnlVentas.Controls.Add(pnlBD);
            pnlVentas.Controls.Add(pnlBotones);
            pnlVentas.Controls.Add(pnlFormulario);
            pnlVentas.Location = new Point(-4, 1);
            pnlVentas.Name = "pnlVentas";
            pnlVentas.Size = new Size(1315, 600);
            pnlVentas.TabIndex = 0;
            pnlVentas.Resize += pnlVentas_Resize;
            // 
            // pnlBD
            // 
            pnlBD.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlBD.BackColor = Color.White;
            pnlBD.Controls.Add(dgvVentas);
            pnlBD.Location = new Point(380, 120);
            pnlBD.Name = "pnlBD";
            pnlBD.Size = new Size(924, 468);
            pnlBD.TabIndex = 2;
            pnlBD.Paint += pnlBD_Paint;
            pnlBD.Resize += pnlBD_Resize;
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToResizeColumns = false;
            dgvVentas.AllowUserToResizeRows = false;
            dgvVentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.BorderStyle = BorderStyle.None;
            dgvVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(234, 243, 222);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(85, 125, 70);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvVentas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvVentas.ColumnHeadersHeight = 45;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVentas.Columns.AddRange(new DataGridViewColumn[] { colVenta, colTelefono, colCantidad, colImporte, colTpago, colCorte, colEstado, colUsuario, colFecha });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(245, 245, 245);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(80, 80, 80);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvVentas.DefaultCellStyle = dataGridViewCellStyle2;
            dgvVentas.EnableHeadersVisualStyles = false;
            dgvVentas.GridColor = Color.FromArgb(235, 235, 235);
            dgvVentas.Location = new Point(0, 0);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.RowTemplate.Height = 50;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(924, 468);
            dgvVentas.TabIndex = 0;
            dgvVentas.CellClick += dgvVentas_CellClick;
            dgvVentas.CellContentClick += dgvVentas_CellContentClick;
            dgvVentas.CellDoubleClick += dgvVentas_CellDoubleClick;
            dgvVentas.CellFormatting += dgvVentas_CellFormatting;
            dgvVentas.CellValueChanged += dgvVentas_CellValueChanged;
            dgvVentas.CurrentCellDirtyStateChanged += dgvVentas_CurrentCellDirtyStateChanged;
            // 
            // colVenta
            // 
            colVenta.HeaderText = "N° Venta";
            colVenta.MinimumWidth = 6;
            colVenta.Name = "colVenta";
            colVenta.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.HeaderText = "Telefono";
            colTelefono.MinimumWidth = 6;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 6;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colImporte
            // 
            colImporte.HeaderText = "Importe";
            colImporte.MinimumWidth = 6;
            colImporte.Name = "colImporte";
            colImporte.ReadOnly = true;
            // 
            // colTpago
            // 
            colTpago.HeaderText = "Pago";
            colTpago.MinimumWidth = 6;
            colTpago.Name = "colTpago";
            colTpago.ReadOnly = true;
            // 
            // colCorte
            // 
            colCorte.HeaderText = "Corte";
            colCorte.MinimumWidth = 6;
            colCorte.Name = "colCorte";
            colCorte.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 6;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colUsuario
            // 
            colUsuario.HeaderText = "Usuario";
            colUsuario.MinimumWidth = 6;
            colUsuario.Name = "colUsuario";
            colUsuario.ReadOnly = true;
            // 
            // colFecha
            // 
            colFecha.HeaderText = "Fecha";
            colFecha.MinimumWidth = 6;
            colFecha.Name = "colFecha";
            colFecha.ReadOnly = true;
            // 
            // pnlBotones
            // 
            pnlBotones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBotones.BackColor = Color.White;
            pnlBotones.Controls.Add(lblEstado);
            pnlBotones.Controls.Add(cmbEstado);
            pnlBotones.Controls.Add(checkFecha);
            pnlBotones.Controls.Add(lblNCliente);
            pnlBotones.Controls.Add(lblNVenta);
            pnlBotones.Controls.Add(textBuscarN);
            pnlBotones.Controls.Add(textBuscarU);
            pnlBotones.Controls.Add(lblNumero);
            pnlBotones.Controls.Add(lblUsuario);
            pnlBotones.Controls.Add(btnFiltrar);
            pnlBotones.Controls.Add(dtpHasta);
            pnlBotones.Controls.Add(dtpDesde);
            pnlBotones.Location = new Point(380, 11);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(924, 100);
            pnlBotones.TabIndex = 1;
            pnlBotones.Paint += pnlBotones_Paint;
            pnlBotones.Resize += pnlBotones_Resize;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Century Gothic", 12.8F);
            lblEstado.ForeColor = Color.FromArgb(59, 109, 17);
            lblEstado.Location = new Point(498, 19);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(91, 25);
            lblEstado.TabIndex = 59;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Items.AddRange(new object[] { "Todos", "Entregado", "Pendiente", "Cancelado" });
            cmbEstado.Location = new Point(595, 20);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 58;
            // 
            // checkFecha
            // 
            checkFecha.AutoSize = true;
            checkFecha.Location = new Point(752, 24);
            checkFecha.Name = "checkFecha";
            checkFecha.Size = new Size(75, 24);
            checkFecha.TabIndex = 1;
            checkFecha.Text = "Fechas";
            checkFecha.UseVisualStyleBackColor = true;
            checkFecha.CheckedChanged += checkFecha_CheckedChanged;
            // 
            // lblNCliente
            // 
            lblNCliente.AutoSize = true;
            lblNCliente.Font = new Font("Century Gothic", 12.8F);
            lblNCliente.ForeColor = Color.FromArgb(59, 109, 17);
            lblNCliente.Location = new Point(235, 16);
            lblNCliente.Name = "lblNCliente";
            lblNCliente.Size = new Size(126, 25);
            lblNCliente.TabIndex = 54;
            lblNCliente.Text = "N° Cliente:";
            // 
            // lblNVenta
            // 
            lblNVenta.AutoSize = true;
            lblNVenta.Font = new Font("Century Gothic", 12.8F);
            lblNVenta.ForeColor = Color.FromArgb(59, 109, 17);
            lblNVenta.Location = new Point(6, 14);
            lblNVenta.Name = "lblNVenta";
            lblNVenta.Size = new Size(116, 25);
            lblNVenta.TabIndex = 57;
            lblNVenta.Text = "N° Venta:";
            // 
            // textBuscarN
            // 
            textBuscarN.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBuscarN.BackColor = Color.FromArgb(250, 254, 247);
            textBuscarN.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBuscarN.ForeColor = Color.DimGray;
            textBuscarN.Location = new Point(367, 14);
            textBuscarN.MaxLength = 10;
            textBuscarN.Name = "textBuscarN";
            textBuscarN.PlaceholderText = "9993546646";
            textBuscarN.Size = new Size(125, 32);
            textBuscarN.TabIndex = 56;
            textBuscarN.KeyPress += textBuscarN_KeyPress;
            // 
            // textBuscarU
            // 
            textBuscarU.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBuscarU.BackColor = Color.FromArgb(250, 254, 247);
            textBuscarU.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBuscarU.ForeColor = Color.DimGray;
            textBuscarU.Location = new Point(128, 12);
            textBuscarU.MaxLength = 15;
            textBuscarU.Name = "textBuscarU";
            textBuscarU.PlaceholderText = "12300123";
            textBuscarU.Size = new Size(101, 32);
            textBuscarU.TabIndex = 55;
            textBuscarU.KeyPress += textBuscarU_KeyPress;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(280, 16);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(0, 20);
            lblNumero.TabIndex = 6;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(18, 16);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(0, 20);
            lblUsuario.TabIndex = 3;
            // 
            // btnFiltrar
            // 
            btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.None;
            btnFiltrar.IconColor = Color.Black;
            btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFiltrar.Location = new Point(831, 20);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(90, 28);
            btnFiltrar.TabIndex = 2;
            btnFiltrar.Text = "Buscar";
            btnFiltrar.Click += btnFiltrar_Click;
            btnFiltrar.Paint += btnFiltrar_Paint;
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(379, 61);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(200, 27);
            dtpHasta.TabIndex = 1;
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(107, 61);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(200, 27);
            dtpDesde.TabIndex = 0;
            // 
            // pnlFormulario
            // 
            pnlFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Location = new Point(16, 11);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(358, 577);
            pnlFormulario.TabIndex = 0;
            pnlFormulario.Paint += pnlFormulario_Paint;
            pnlFormulario.Resize += pnlFormulario_Resize;
            // 
            // FormVentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1312, 601);
            Controls.Add(pnlVentas);
            Name = "FormVentas";
            Text = "FormVentas";
            Load += FormVentas_Load;
            pnlVentas.ResumeLayout(false);
            pnlBD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlVentas;
        private Panel pnlFormulario;
        private Panel pnlBD;
        private Panel pnlBotones;
        private DataGridView dgvVentas;
        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private Label lblNumero;
        private Label lblUsuario;
        private TextBox textBuscarN;
        private TextBox textBuscarU;
        private Label lblNVenta;
        private CheckBox checkFecha;
        private Label lblNCliente;
        private DataGridViewTextBoxColumn colVenta;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colImporte;
        private DataGridViewTextBoxColumn colTpago;
        private DataGridViewTextBoxColumn colCorte;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colUsuario;
        private DataGridViewTextBoxColumn colFecha;
        private ComboBox cmbEstado;
        private Label lblEstado;
    }
}