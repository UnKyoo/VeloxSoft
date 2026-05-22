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
            colTelefono = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colImporte = new DataGridViewTextBoxColumn();
            colTpago = new DataGridViewTextBoxColumn();
            colCorte = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colUsuario = new DataGridViewTextBoxColumn();
            colFecha = new DataGridViewTextBoxColumn();
            pnlBotones = new Panel();
            checkFecha = new CheckBox();
            lblBuscarN = new Label();
            lblBuscarU = new Label();
            textBuscarN = new TextBox();
            textBuscarU = new TextBox();
            lblNumero = new Label();
            lblUsuario = new Label();
            btnFiltrar = new FontAwesome.Sharp.IconButton();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            pnlFormulario = new Panel();
            label1 = new Label();
            pnlVentas.SuspendLayout();
            pnlBD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            pnlBotones.SuspendLayout();
            pnlFormulario.SuspendLayout();
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
            pnlVentas.Size = new Size(1122, 597);
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
            pnlBD.Size = new Size(731, 465);
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
            dgvVentas.Columns.AddRange(new DataGridViewColumn[] { colTelefono, colCantidad, colImporte, colTpago, colCorte, colEstado, colUsuario, colFecha });
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
            dgvVentas.Size = new Size(731, 465);
            dgvVentas.TabIndex = 0;
            dgvVentas.CellClick += dgvVentas_CellClick;
            dgvVentas.CellContentClick += dgvVentas_CellContentClick;
            dgvVentas.CellDoubleClick += dgvVentas_CellDoubleClick;
            dgvVentas.CellFormatting += dgvVentas_CellFormatting;
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
            colTpago.HeaderText = "Tipo de pago";
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
            pnlBotones.Controls.Add(checkFecha);
            pnlBotones.Controls.Add(lblBuscarN);
            pnlBotones.Controls.Add(lblBuscarU);
            pnlBotones.Controls.Add(textBuscarN);
            pnlBotones.Controls.Add(textBuscarU);
            pnlBotones.Controls.Add(lblNumero);
            pnlBotones.Controls.Add(lblUsuario);
            pnlBotones.Controls.Add(btnFiltrar);
            pnlBotones.Controls.Add(dtpHasta);
            pnlBotones.Controls.Add(dtpDesde);
            pnlBotones.Location = new Point(380, 11);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(731, 100);
            pnlBotones.TabIndex = 1;
            pnlBotones.Paint += pnlBotones_Paint;
            pnlBotones.Resize += pnlBotones_Resize;
            // 
            // checkFecha
            // 
            checkFecha.AutoSize = true;
            checkFecha.Location = new Point(586, 44);
            checkFecha.Name = "checkFecha";
            checkFecha.Size = new Size(142, 24);
            checkFecha.TabIndex = 1;
            checkFecha.Text = "Filtrar por fechas";
            checkFecha.UseVisualStyleBackColor = true;
            checkFecha.CheckedChanged += checkFecha_CheckedChanged;
            // 
            // lblBuscarN
            // 
            lblBuscarN.AutoSize = true;
            lblBuscarN.Font = new Font("Century Gothic", 12.8F);
            lblBuscarN.ForeColor = Color.FromArgb(59, 109, 17);
            lblBuscarN.Location = new Point(312, 14);
            lblBuscarN.Name = "lblBuscarN";
            lblBuscarN.Size = new Size(104, 25);
            lblBuscarN.TabIndex = 54;
            lblBuscarN.Text = "Numero:";
            // 
            // lblBuscarU
            // 
            lblBuscarU.AutoSize = true;
            lblBuscarU.Font = new Font("Century Gothic", 12.8F);
            lblBuscarU.ForeColor = Color.FromArgb(59, 109, 17);
            lblBuscarU.Location = new Point(6, 14);
            lblBuscarU.Name = "lblBuscarU";
            lblBuscarU.Size = new Size(95, 25);
            lblBuscarU.TabIndex = 57;
            lblBuscarU.Text = "Usuario:";
            // 
            // textBuscarN
            // 
            textBuscarN.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBuscarN.BackColor = Color.FromArgb(250, 254, 247);
            textBuscarN.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBuscarN.ForeColor = Color.DimGray;
            textBuscarN.Location = new Point(437, 10);
            textBuscarN.Name = "textBuscarN";
            textBuscarN.PlaceholderText = "9993546646";
            textBuscarN.Size = new Size(142, 32);
            textBuscarN.TabIndex = 56;
            // 
            // textBuscarU
            // 
            textBuscarU.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBuscarU.BackColor = Color.FromArgb(250, 254, 247);
            textBuscarU.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBuscarU.ForeColor = Color.DimGray;
            textBuscarU.Location = new Point(107, 10);
            textBuscarU.Name = "textBuscarU";
            textBuscarU.PlaceholderText = "Argel";
            textBuscarU.Size = new Size(142, 32);
            textBuscarU.TabIndex = 55;
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
            btnFiltrar.Location = new Point(624, 10);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(90, 28);
            btnFiltrar.TabIndex = 2;
            btnFiltrar.Text = "Buscar";
            btnFiltrar.Click += btnFiltrar_Click;
            btnFiltrar.Paint += btnFiltrar_Paint;
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(327, 61);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(200, 27);
            dtpHasta.TabIndex = 1;
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(71, 61);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(200, 27);
            dtpDesde.TabIndex = 0;
            // 
            // pnlFormulario
            // 
            pnlFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(label1);
            pnlFormulario.Location = new Point(16, 11);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(358, 574);
            pnlFormulario.TabIndex = 0;
            pnlFormulario.Paint += pnlFormulario_Paint;
            pnlFormulario.Resize += pnlFormulario_Resize;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(135, 176);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // FormVentas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1119, 598);
            Controls.Add(pnlVentas);
            Name = "FormVentas";
            Text = "FormVentas";
            pnlVentas.ResumeLayout(false);
            pnlBD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
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
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colImporte;
        private DataGridViewTextBoxColumn colTpago;
        private DataGridViewTextBoxColumn colCorte;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colUsuario;
        private DataGridViewTextBoxColumn colFecha;
        private Label label1;
        private Label lblNumero;
        private Label lblUsuario;
        private TextBox textBuscarN;
        private TextBox textBuscarU;
        private Label lblBuscarU;
        private CheckBox checkFecha;
        private Label lblBuscarN;
    }
}