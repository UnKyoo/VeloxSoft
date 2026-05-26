namespace VeloxSoft.Formularios
{
    partial class FormGastos
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            pnlGastos = new Panel();
            pnlFormulario = new Panel();
            LabelError = new Label();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            btnGuardar = new Button();
            btnAgregarProveedor = new Button();
            txtMonto = new TextBox();
            cbProveedor = new ComboBox();
            lblMonto = new Label();
            lblProveedor = new Label();
            lblTituloForm = new Label();
            pnlBotones = new Panel();
            btnBuscarU = new FontAwesome.Sharp.IconButton();
            dtHasta = new DateTimePicker();
            dtDesde = new DateTimePicker();
            lblHasta = new Label();
            lblDesde = new Label();
            cbProveedorFiltro = new ComboBox();
            lblProveedorFiltro = new Label();
            cbTablas = new ComboBox();
            lblTabla = new Label();
            pnlBD = new Panel();
            pnlGastos.SuspendLayout();
            pnlFormulario.SuspendLayout();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlGastos
            // 
            pnlGastos.BackColor = Color.FromArgb(245, 247, 242);
            pnlGastos.Controls.Add(pnlFormulario);
            pnlGastos.Controls.Add(pnlBotones);
            pnlGastos.Controls.Add(pnlBD);
            pnlGastos.Dock = DockStyle.Fill;
            pnlGastos.Location = new Point(0, 0);
            pnlGastos.Margin = new Padding(3, 4, 3, 4);
            pnlGastos.Name = "pnlGastos";
            pnlGastos.Size = new Size(1600, 1067);
            pnlGastos.TabIndex = 0;
            pnlGastos.Resize += pnlGastos_Resize;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = Color.White;
            pnlFormulario.Controls.Add(LabelError);
            pnlFormulario.Controls.Add(btnEliminar);
            pnlFormulario.Controls.Add(btnLimpiar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(btnAgregarProveedor);
            pnlFormulario.Controls.Add(txtMonto);
            pnlFormulario.Controls.Add(cbProveedor);
            pnlFormulario.Controls.Add(lblMonto);
            pnlFormulario.Controls.Add(lblProveedor);
            pnlFormulario.Controls.Add(lblTituloForm);
            pnlFormulario.Location = new Point(14, 16);
            pnlFormulario.Margin = new Padding(3, 4, 3, 4);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(571, 1013);
            pnlFormulario.TabIndex = 0;
            pnlFormulario.Paint += pnlFormulario_Paint;
            pnlFormulario.Resize += pnlFormulario_Resize;
            // 
            // LabelError
            // 
            LabelError.Font = new Font("Century Gothic", 9F);
            LabelError.ForeColor = Color.Red;
            LabelError.Location = new Point(46, 453);
            LabelError.Name = "LabelError";
            LabelError.Size = new Size(448, 53);
            LabelError.TabIndex = 11;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(180, 45, 45);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Century Gothic", 12F);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(279, 900);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(215, 60);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Paint += btnEliminar_Paint;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(226, 233, 214);
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Century Gothic", 12F);
            btnLimpiar.ForeColor = Color.FromArgb(59, 109, 17);
            btnLimpiar.Location = new Point(46, 900);
            btnLimpiar.Margin = new Padding(3, 4, 3, 4);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(215, 60);
            btnLimpiar.TabIndex = 9;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            btnLimpiar.Paint += btnLimpiar_Paint;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(59, 109, 17);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Century Gothic", 12F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(46, 813);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(448, 64);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            btnGuardar.Paint += btnGuardar_Paint;
            // 
            // btnAgregarProveedor
            // 
            btnAgregarProveedor.BackColor = Color.FromArgb(59, 109, 17);
            btnAgregarProveedor.FlatAppearance.BorderSize = 0;
            btnAgregarProveedor.FlatStyle = FlatStyle.Flat;
            btnAgregarProveedor.Font = new Font("Century Gothic", 14F, FontStyle.Bold);
            btnAgregarProveedor.ForeColor = Color.White;
            btnAgregarProveedor.Location = new Point(446, 157);
            btnAgregarProveedor.Margin = new Padding(3, 4, 3, 4);
            btnAgregarProveedor.Name = "btnAgregarProveedor";
            btnAgregarProveedor.Size = new Size(48, 43);
            btnAgregarProveedor.TabIndex = 3;
            btnAgregarProveedor.Text = "+";
            btnAgregarProveedor.UseVisualStyleBackColor = false;
            btnAgregarProveedor.Click += btnAgregarProveedor_Click;
            // 
            // txtMonto
            // 
            txtMonto.BackColor = Color.FromArgb(250, 254, 247);
            txtMonto.Font = new Font("Century Gothic", 12F);
            txtMonto.Location = new Point(46, 277);
            txtMonto.Margin = new Padding(3, 4, 3, 4);
            txtMonto.MaxLength = 10;
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(447, 32);
            txtMonto.TabIndex = 5;
            txtMonto.TextChanged += txtMonto_TextChanged;
            txtMonto.KeyPress += txtMonto_KeyPress;
            // 
            // cbProveedor
            // 
            cbProveedor.BackColor = Color.FromArgb(250, 254, 247);
            cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProveedor.Font = new Font("Century Gothic", 12F);
            cbProveedor.FormattingEnabled = true;
            cbProveedor.Location = new Point(46, 157);
            cbProveedor.Margin = new Padding(3, 4, 3, 4);
            cbProveedor.Name = "cbProveedor";
            cbProveedor.Size = new Size(388, 31);
            cbProveedor.TabIndex = 2;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Century Gothic", 12F);
            lblMonto.ForeColor = Color.FromArgb(59, 109, 17);
            lblMonto.Location = new Point(46, 240);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(72, 23);
            lblMonto.TabIndex = 4;
            lblMonto.Text = "Monto";
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Century Gothic", 12F);
            lblProveedor.ForeColor = Color.FromArgb(59, 109, 17);
            lblProveedor.Location = new Point(46, 120);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(113, 23);
            lblProveedor.TabIndex = 1;
            lblProveedor.Text = "Proveedor";
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Century Gothic", 24F, FontStyle.Bold);
            lblTituloForm.ForeColor = Color.Black;
            lblTituloForm.Location = new Point(34, 27);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(154, 47);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Gastos";
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.White;
            pnlBotones.Controls.Add(btnBuscarU);
            pnlBotones.Controls.Add(dtHasta);
            pnlBotones.Controls.Add(dtDesde);
            pnlBotones.Controls.Add(lblHasta);
            pnlBotones.Controls.Add(lblDesde);
            pnlBotones.Controls.Add(cbProveedorFiltro);
            pnlBotones.Controls.Add(lblProveedorFiltro);
            pnlBotones.Controls.Add(cbTablas);
            pnlBotones.Controls.Add(lblTabla);
            pnlBotones.Location = new Point(606, 16);
            pnlBotones.Margin = new Padding(3, 4, 3, 4);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Size = new Size(971, 133);
            pnlBotones.TabIndex = 1;
            pnlBotones.Paint += pnlBotones_Paint;
            pnlBotones.Resize += pnlBotones_Resize;
            // 
            // btnBuscarU
            // 
            btnBuscarU.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnBuscarU.BackColor = Color.FromArgb(59, 109, 17);
            btnBuscarU.FlatAppearance.BorderSize = 0;
            btnBuscarU.FlatStyle = FlatStyle.Flat;
            btnBuscarU.IconChar = FontAwesome.Sharp.IconChar.Search;
            btnBuscarU.IconColor = Color.Black;
            btnBuscarU.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBuscarU.IconSize = 25;
            btnBuscarU.Location = new Point(691, 73);
            btnBuscarU.Name = "btnBuscarU";
            btnBuscarU.Size = new Size(37, 34);
            btnBuscarU.TabIndex = 42;
            btnBuscarU.UseVisualStyleBackColor = false;
            btnBuscarU.Click += btnBuscarU_Click;
            // 
            // dtHasta
            // 
            dtHasta.Font = new Font("Century Gothic", 10F);
            dtHasta.Location = new Point(424, 75);
            dtHasta.Margin = new Padding(3, 4, 3, 4);
            dtHasta.Name = "dtHasta";
            dtHasta.Size = new Size(228, 28);
            dtHasta.TabIndex = 7;
            // 
            // dtDesde
            // 
            dtDesde.Font = new Font("Century Gothic", 10F);
            dtDesde.Location = new Point(95, 75);
            dtDesde.Margin = new Padding(3, 4, 3, 4);
            dtDesde.Name = "dtDesde";
            dtDesde.Size = new Size(228, 28);
            dtDesde.TabIndex = 5;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Century Gothic", 12F);
            lblHasta.ForeColor = Color.FromArgb(59, 109, 17);
            lblHasta.Location = new Point(354, 77);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(67, 23);
            lblHasta.TabIndex = 6;
            lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Century Gothic", 12F);
            lblDesde.ForeColor = Color.FromArgb(59, 109, 17);
            lblDesde.Location = new Point(17, 77);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(73, 23);
            lblDesde.TabIndex = 4;
            lblDesde.Text = "Desde";
            // 
            // cbProveedorFiltro
            // 
            cbProveedorFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbProveedorFiltro.Font = new Font("Century Gothic", 11F);
            cbProveedorFiltro.FormattingEnabled = true;
            cbProveedorFiltro.Location = new Point(471, 16);
            cbProveedorFiltro.Margin = new Padding(3, 4, 3, 4);
            cbProveedorFiltro.Name = "cbProveedorFiltro";
            cbProveedorFiltro.Size = new Size(228, 29);
            cbProveedorFiltro.TabIndex = 3;
            // 
            // lblProveedorFiltro
            // 
            lblProveedorFiltro.AutoSize = true;
            lblProveedorFiltro.Font = new Font("Century Gothic", 12F);
            lblProveedorFiltro.ForeColor = Color.FromArgb(59, 109, 17);
            lblProveedorFiltro.Location = new Point(346, 20);
            lblProveedorFiltro.Name = "lblProveedorFiltro";
            lblProveedorFiltro.Size = new Size(113, 23);
            lblProveedorFiltro.TabIndex = 2;
            lblProveedorFiltro.Text = "Proveedor";
            // 
            // cbTablas
            // 
            cbTablas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTablas.Font = new Font("Century Gothic", 11F);
            cbTablas.FormattingEnabled = true;
            cbTablas.Location = new Point(91, 16);
            cbTablas.Margin = new Padding(3, 4, 3, 4);
            cbTablas.Name = "cbTablas";
            cbTablas.Size = new Size(205, 29);
            cbTablas.TabIndex = 1;
            // 
            // lblTabla
            // 
            lblTabla.AutoSize = true;
            lblTabla.Font = new Font("Century Gothic", 12F);
            lblTabla.ForeColor = Color.FromArgb(59, 109, 17);
            lblTabla.Location = new Point(17, 20);
            lblTabla.Name = "lblTabla";
            lblTabla.Size = new Size(65, 23);
            lblTabla.TabIndex = 0;
            lblTabla.Text = "Tabla";
            // 
            // pnlBD
            // 
            pnlBD.BackColor = Color.White;
            pnlBD.Location = new Point(606, 167);
            pnlBD.Margin = new Padding(3, 4, 3, 4);
            pnlBD.Name = "pnlBD";
            pnlBD.Size = new Size(971, 863);
            pnlBD.TabIndex = 2;
            pnlBD.Paint += pnlBD_Paint;
            // 
            // FormGastos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 242);
            ClientSize = new Size(1600, 1067);
            Controls.Add(pnlGastos);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormGastos";
            Text = "FormGastos";
            pnlGastos.ResumeLayout(false);
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            pnlBotones.ResumeLayout(false);
            pnlBotones.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlGastos;
        private Panel pnlFormulario;
        private Panel pnlBotones;
        private Panel pnlBD;

        private Label lblTituloForm;
        private Label lblProveedor;
        private Label lblMonto;

        private ComboBox cbProveedor;
        private TextBox txtMonto;

        private Button btnAgregarProveedor;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Button btnEliminar;

        private Label LabelError;

        private Label lblTabla;
        private ComboBox cbTablas;

        private Label lblProveedorFiltro;
        private ComboBox cbProveedorFiltro;

        private Label lblDesde;
        private Label lblHasta;

        private DateTimePicker dtDesde;
        private DateTimePicker dtHasta;
        private FontAwesome.Sharp.IconButton btnBuscarU;
    }
}