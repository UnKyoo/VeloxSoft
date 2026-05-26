    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using VeloxSoft.Formularios;
    using VeloxSoft.Services;
    using VeloxSoft.Models;

namespace VeloxSoft.Formularios
    {
        public partial class FormGastos : Form
        {
            private Panel? pnlMiniFormulario;
            private Panel? pnlBarraMiniFormulario;
            private Button? btnCerrarMiniFormulario;

            private bool moviendoMiniFormulario = false;
            private Point puntoInicioMouse;
            private Point puntoInicioPanel;

            private FormTablaGastos? _formTablaGastos;
            private FormTablaProveedores? _formTablaProveedores;
            private Form? _formActual;
            private readonly ServicioGasto _ServicioGasto;
      

            public FormGastos(ServicioGasto ServicioGasto)
            {
                InitializeComponent();
                _ServicioGasto = ServicioGasto;
                CrearMiniFormulario();
                InicializarFormulariosBD();
                InicializarFiltros();

                this.DoubleBuffered = true;

                pnlFormulario_Resize(this, EventArgs.Empty);
                pnlBotones_Resize(this, EventArgs.Empty);
                pnlGastos_Resize(this, EventArgs.Empty);

                this.Load += FormGastos_Load; // cargar datos al abrir el formulario
            }


            private void FormGastos_Load(object sender, EventArgs e)
            {


                // Cargar proveedores en el filtro
                var proveedores = _ServicioGasto.Buscar_Gastos(out string err);

                // cbProveedor con objetos Gasto
                cbProveedor.Items.Clear();
                cbProveedor.DisplayMember = "Descripcion"; // ← muestra el nombre
                cbProveedor.ValueMember = "IdGasto";        // ← guarda el ID
                foreach (var p in proveedores)
                    cbProveedor.Items.Add(p);


                cbProveedorFiltro.Items.Clear();
                cbProveedorFiltro.Items.Add("Todos");
                foreach (var p in proveedores)
                    cbProveedorFiltro.Items.Add(p.Descripcion);
                cbProveedorFiltro.SelectedIndex = 0;

                _formTablaGastos?.CargarDatos(
                    dtDesde.Value.ToString("yyyy-MM-dd"),
                    dtHasta.Value.ToString("yyyy-MM-dd"),
                    "Todos"
                );
                _formTablaProveedores?.CargarDatos();
            }

            // ---------------- MINI FORMULARIO ----------------

            private void CrearMiniFormulario()
            {
                pnlMiniFormulario = new Panel();
                btnCerrarMiniFormulario = new Button();

                pnlMiniFormulario.Name = "pnlMiniFormulario";
                pnlMiniFormulario.Size = new Size(400, 250);
                pnlMiniFormulario.BackColor = Color.White;
                pnlMiniFormulario.BorderStyle = BorderStyle.None;
                pnlMiniFormulario.Visible = false;

                pnlMiniFormulario.Location = new Point(
                    (this.ClientSize.Width - pnlMiniFormulario.Width) / 2,
                    (this.ClientSize.Height - pnlMiniFormulario.Height) / 2
                );

                // Barra superior
                pnlBarraMiniFormulario = new Panel();
                pnlBarraMiniFormulario.Height = 36;
                pnlBarraMiniFormulario.Dock = DockStyle.Top;
                pnlBarraMiniFormulario.BackColor = Color.FromArgb(234, 243, 222);
                pnlBarraMiniFormulario.MouseDown += pnlMiniFormulario_MouseDown;
                pnlBarraMiniFormulario.MouseMove += pnlMiniFormulario_MouseMove;
                pnlBarraMiniFormulario.MouseUp += pnlMiniFormulario_MouseUp;

                Label lblTituloMini = new Label();
                lblTituloMini.AutoSize = true;
                lblTituloMini.Text = "Agregar proveedor";
                lblTituloMini.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
                lblTituloMini.ForeColor = Color.FromArgb(59, 109, 17);
                lblTituloMini.Location = new Point(12, 8);

                pnlBarraMiniFormulario.Controls.Add(lblTituloMini);
                pnlMiniFormulario.Controls.Add(pnlBarraMiniFormulario);

                Font fuente = new Font("Century Gothic", 12F);
                Color colorLabel = Color.FromArgb(59, 109, 17);
                Color colorInput = Color.FromArgb(250, 254, 247);

                Label lblProveedor = new Label();
                lblProveedor.Text = "Proveedor";
                lblProveedor.Font = fuente;
                lblProveedor.ForeColor = colorLabel;
                lblProveedor.AutoSize = true;
                lblProveedor.Location = new Point(30, 70);

                TextBox txtProveedor = new TextBox();
                txtProveedor.Name = "txtProveedor";
                txtProveedor.Font = fuente;
                txtProveedor.BackColor = colorInput;
                txtProveedor.Location = new Point(30, 100);
                txtProveedor.Size = new Size(330, 32);

                Label lblMensaje = new Label();
                lblMensaje.Name = "lblMensajeProveedor";
                lblMensaje.AutoSize = false;
                lblMensaje.Size = new Size(330, 20);
                lblMensaje.Location = new Point(30, 145);
                lblMensaje.Font = new Font("Century Gothic", 9F);

                Button btnGuardarMini = new Button();
                btnGuardarMini.Text = "Guardar proveedor";
                btnGuardarMini.Font = fuente;
                btnGuardarMini.ForeColor = Color.White;
                btnGuardarMini.BackColor = Color.FromArgb(59, 109, 17);
                btnGuardarMini.FlatStyle = FlatStyle.Flat;
                btnGuardarMini.FlatAppearance.BorderSize = 0;
                btnGuardarMini.Location = new Point(95, 180);
                btnGuardarMini.Size = new Size(200, 40);

            btnGuardarMini.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtProveedor.Text))
                {
                    lblMensaje.Text = "Escribe el nombre del proveedor.";
                    lblMensaje.ForeColor = Color.Red;
                    return;
                }

                bool ok = _ServicioGasto.NombreGasto(0, txtProveedor.Text.Trim(), out string error);

                if (!ok)
                {
                    lblMensaje.Text = error;
                    lblMensaje.ForeColor = Color.Red;
                    return;
                }

                // Recargar proveedores
                var proveedores = _ServicioGasto.Buscar_Gastos(out string err);

                cbProveedor.Items.Clear();
                cbProveedor.DisplayMember = "Descripcion";
                cbProveedor.ValueMember = "IdGasto";
                foreach (var p in proveedores)
                    cbProveedor.Items.Add(p);

                cbProveedorFiltro.Items.Clear();
                cbProveedorFiltro.Items.Add("Todos");
                foreach (var p in proveedores)
                    cbProveedorFiltro.Items.Add(p.Descripcion);
                cbProveedorFiltro.SelectedIndex = 0;

                _formTablaProveedores?.CargarDatos();

                lblMensaje.Text = "Proveedor agregado correctamente.";
                lblMensaje.ForeColor = Color.Green;

                txtProveedor.Text = "";
                pnlMiniFormulario.Visible = false;
            };

            // Botón cerrar
            btnCerrarMiniFormulario.Text = "X";
                btnCerrarMiniFormulario.Size = new Size(30, 26);
                btnCerrarMiniFormulario.Location = new Point(355, 5);
                btnCerrarMiniFormulario.FlatStyle = FlatStyle.Flat;
                btnCerrarMiniFormulario.FlatAppearance.BorderSize = 0;
                btnCerrarMiniFormulario.BackColor = Color.FromArgb(163, 45, 45);
                btnCerrarMiniFormulario.ForeColor = Color.White;
                btnCerrarMiniFormulario.Click += btnCerrarMiniFormulario_Click;

                pnlBarraMiniFormulario.Controls.Add(btnCerrarMiniFormulario);

                pnlMiniFormulario.Controls.Add(lblProveedor);
                pnlMiniFormulario.Controls.Add(txtProveedor);
                pnlMiniFormulario.Controls.Add(lblMensaje);
                pnlMiniFormulario.Controls.Add(btnGuardarMini);

                this.Controls.Add(pnlMiniFormulario);
                pnlMiniFormulario.BringToFront();
            }

            private void btnAgregarProveedor_Click(object sender, EventArgs e)
            {
                if (pnlMiniFormulario != null)
                {
                    pnlMiniFormulario.Visible = true;
                    pnlMiniFormulario.BringToFront();
                }
            }

            private void btnCerrarMiniFormulario_Click(object? sender, EventArgs e)
            {
                if (pnlMiniFormulario != null)
                    pnlMiniFormulario.Visible = false;
            }

            private void pnlMiniFormulario_MouseDown(object? sender, MouseEventArgs e)
            {
                if (pnlMiniFormulario == null) return;

                if (e.Button == MouseButtons.Left)
                {
                    moviendoMiniFormulario = true;
                    puntoInicioMouse = Cursor.Position;
                    puntoInicioPanel = pnlMiniFormulario.Location;
                }
            }

            private void pnlMiniFormulario_MouseMove(object? sender, MouseEventArgs e)
            {
                if (!moviendoMiniFormulario || pnlMiniFormulario == null) return;

                Point delta = new Point(
                    Cursor.Position.X - puntoInicioMouse.X,
                    Cursor.Position.Y - puntoInicioMouse.Y
                );

                pnlMiniFormulario.Location = new Point(
                    puntoInicioPanel.X + delta.X,
                    puntoInicioPanel.Y + delta.Y
                );
            }

            private void pnlMiniFormulario_MouseUp(object? sender, MouseEventArgs e)
            {
                moviendoMiniFormulario = false;
            }

            // ---------------- TABLAS ----------------

            private void InicializarFormulariosBD()
            {
                _formTablaGastos = new FormTablaGastos(_ServicioGasto);
                _formTablaProveedores = new FormTablaProveedores(_ServicioGasto);

                // ← Estas líneas son las que faltan
                _formTablaGastos.TopLevel = false;
                _formTablaGastos.FormBorderStyle = FormBorderStyle.None;
                _formTablaGastos.Dock = DockStyle.Fill;

                _formTablaProveedores.TopLevel = false;
                _formTablaProveedores.FormBorderStyle = FormBorderStyle.None;
                _formTablaProveedores.Dock = DockStyle.Fill;

                pnlBD.Controls.Add(_formTablaGastos);
                pnlBD.Controls.Add(_formTablaProveedores);

                _formTablaProveedores.Hide();
                _formTablaGastos.Show();

                _formActual = _formTablaGastos;
            }

            private void MostrarFormEnPanel(Form form)
            {
                if (_formActual != null)
                    _formActual.Hide();

                if (!pnlBD.Controls.Contains(form))
                {
                    form.TopLevel = false;                        // ← agrega esto
                    form.FormBorderStyle = FormBorderStyle.None;  // ← y esto
                    form.Dock = DockStyle.Fill;                   // ← y esto
                    pnlBD.Controls.Add(form);
                }

                form.Show();
                _formActual = form;
            }

            private void InicializarFiltros()
            {
                cbTablas.Items.Clear();
                cbTablas.Items.Add("Gastos");
                cbTablas.Items.Add("Proveedores");
                cbTablas.SelectedIndex = 0;

                cbProveedorFiltro.Items.Clear();
                cbProveedorFiltro.Items.Add("Todos");
                cbProveedorFiltro.SelectedIndex = 0;

                cbTablas.SelectedIndexChanged += cbTablas_SelectedIndexChanged;
            }

            private void cbTablas_SelectedIndexChanged(object? sender, EventArgs e)
            {
                switch (cbTablas.SelectedItem?.ToString())
                {
                    case "Gastos":
                        if (_formTablaGastos != null)
                        {
                            MostrarFormEnPanel(_formTablaGastos);
                            _formTablaGastos.CargarDatos(
                                dtDesde.Value.ToString("yyyy-MM-dd"),
                                dtHasta.Value.ToString("yyyy-MM-dd"),
                                cbProveedorFiltro.SelectedItem?.ToString() ?? "Todos"
                            );
                        }
                        break;

                    case "Proveedores":
                        if (_formTablaProveedores != null)
                        {
                            MostrarFormEnPanel(_formTablaProveedores);
                            _formTablaProveedores.CargarDatos();
                        }
                        break;
                }
            }

            // ---------------- BOTONES ----------------

            private void btnGuardar_Click(object sender, EventArgs e)
            {
                if (cbProveedor.SelectedItem == null)
                {
                    LabelError.Text = "Selecciona un proveedor.";
                    LabelError.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMonto.Text))
                {
                    LabelError.Text = "Ingresa el monto.";
                    LabelError.ForeColor = Color.Red;
                    return;
                }
                Gasto proveedorSeleccionado = (Gasto)cbProveedor.SelectedItem;
                string montoTexto = txtMonto.Text.Replace("$", "").Trim();

                if (!decimal.TryParse(montoTexto, out decimal monto))
                {
                    LabelError.Text = "Monto inválido.";
                    LabelError.ForeColor = Color.Red;
                    return;
                }

                bool ok = _ServicioGasto.AgregarDetalleGasto(monto, proveedorSeleccionado.IdGasto, out string error);

                if (!ok)
                {
                    LabelError.Text = error;
                    LabelError.ForeColor = Color.Red;
                    return;
                }

                // Recargar tabla
                _formTablaGastos?.CargarDatos(
                    dtDesde.Value.ToString("yyyy-MM-dd"),
                    dtHasta.Value.ToString("yyyy-MM-dd"),
                    cbProveedorFiltro.SelectedItem?.ToString() ?? "Todos"
                );


            LabelError.Text = "Gasto registrado correctamente.";
                LabelError.ForeColor = Color.Green;

                LimpiarCampos();
            }

            private void btnLimpiar_Click(object sender, EventArgs e)
            {
                LimpiarCampos();
            }

       

            private void LimpiarCampos()
            {
                cbProveedor.SelectedIndex = -1;
                txtMonto.Text = "";
                LabelError.Text = "";
            }

            private void btnBuscarU_Click(object sender, EventArgs e)
            {
                switch (cbTablas.SelectedItem?.ToString())
                {
                    case "Gastos":
                        _formTablaGastos?.CargarDatos(
                            dtDesde.Value.ToString("yyyy-MM-dd"),
                            dtHasta.Value.ToString("yyyy-MM-dd"),
                            cbProveedorFiltro.SelectedItem?.ToString() ?? "Todos"
                        );
                        break;

                    case "Proveedores":
                        _formTablaProveedores?.CargarDatos();
                        break;
                }
            }

            // ---------------- DISEÑO ----------------

            private void RedondearPanel(Panel p, PaintEventArgs e, int radio)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radio, radio, 180, 90);
                path.AddArc(p.Width - radio, 0, radio, radio, 270, 90);
                path.AddArc(p.Width - radio, p.Height - radio, radio, radio, 0, 90);
                path.AddArc(0, p.Height - radio, radio, radio, 90, 90);
                path.CloseAllFigures();

                p.Region = new Region(path);

                using (Pen pen = new Pen(ColorTranslator.FromHtml("#A4D1A5"), 2))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }

            private void RedondearBoton(Button btn, PaintEventArgs e, int radio)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radio, radio, 180, 90);
                    path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
                    path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
                    path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
                    path.CloseAllFigures();

                    btn.Region = new Region(path);
                }
            }

            private void pnlFormulario_Paint(object sender, PaintEventArgs e)
            {
                RedondearPanel((Panel)sender, e, 15);
            }

            private void pnlBotones_Paint(object sender, PaintEventArgs e)
            {
                RedondearPanel((Panel)sender, e, 15);
            }

            private void pnlBD_Paint(object sender, PaintEventArgs e)
            {
                RedondearPanel((Panel)sender, e, 15);
            }

            private void btnGuardar_Paint(object sender, PaintEventArgs e)
            {
                RedondearBoton(btnGuardar, e, 15);
            }

            private void btnLimpiar_Paint(object sender, PaintEventArgs e)
            {
                RedondearBoton(btnLimpiar, e, 15);
            }

            private void btnEliminar_Paint(object sender, PaintEventArgs e)
            {
                RedondearBoton(btnEliminar, e, 15);
            }

            // ---------------- RESIZE ----------------

            private void pnlGastos_Resize(object sender, EventArgs e)
            {
                int w = pnlGastos.Width;
                int h = pnlGastos.Height;
                int margen = 12;
                int espacioH = 10;

                int anchoIzq = (int)(w * 0.40) - margen;
                int anchoDer = w - anchoIzq - (margen * 3) - espacioH;

                int altoBotones = w < 900 ? 120 : 80;

                pnlFormulario.Location = new Point(margen, margen);
                pnlFormulario.Size = new Size(anchoIzq, h - margen * 2);

                pnlBotones.Location = new Point(anchoIzq + margen * 2, margen);
                pnlBotones.Size = new Size(anchoDer, altoBotones);

                int yBD = margen + altoBotones + espacioH;

                pnlBD.Location = new Point(anchoIzq + margen * 2, yBD);
                pnlBD.Size = new Size(anchoDer, h - yBD - margen);
            }

            private void pnlFormulario_Resize(object sender, EventArgs e)
            {
                int w = pnlFormulario.Width;
                int h = pnlFormulario.Height;
                int margen = 40;
                int anchoInput = w - (margen * 2);
                int altoInput = 32;
                int altoLabel = 23;
                int espacioEntreGrupos = 18;

                lblTituloForm.Location = new Point(margen - 10, 15);

                int yProveedor = 80;
                lblProveedor.Location = new Point(margen, yProveedor);

                int anchoBuscar = 42;
                int espacioBuscar = 8;

                cbProveedor.Location = new Point(margen, yProveedor + altoLabel + 2);
                cbProveedor.Size = new Size(anchoInput - anchoBuscar - espacioBuscar, altoInput);

                btnAgregarProveedor.Location = new Point(cbProveedor.Right + espacioBuscar, cbProveedor.Top);
                btnAgregarProveedor.Size = new Size(anchoBuscar, altoInput);

                int yMonto = cbProveedor.Bottom + espacioEntreGrupos;
                lblMonto.Location = new Point(margen, yMonto);
                txtMonto.Location = new Point(margen, yMonto + altoLabel + 2);
                txtMonto.Size = new Size(anchoInput, altoInput);

                int yFecha = txtMonto.Bottom + espacioEntreGrupos;

                int altoBotones = 48;
                int espacioBtn = 10;
                int yGuardar = h - altoBotones * 2 - espacioBtn - 30;
                int yFilaAbajo = yGuardar + altoBotones + espacioBtn;
                int anchoBtnPeq = (anchoInput - espacioBtn) / 2;

                btnGuardar.Location = new Point(margen, yGuardar);
                btnGuardar.Size = new Size(anchoInput, altoBotones);

                btnLimpiar.Location = new Point(margen, yFilaAbajo);
                btnLimpiar.Size = new Size(anchoBtnPeq, altoBotones);

                btnEliminar.Location = new Point(margen + anchoBtnPeq + espacioBtn, yFilaAbajo);
                btnEliminar.Size = new Size(anchoBtnPeq, altoBotones);
            }

            private void pnlBotones_Resize(object sender, EventArgs e)
            {
                int w = pnlBotones.Width;
                int margenH = 15;
                int altoCtrl = 28;
                int espacioP = 8;
                int espacioG = 20;

                // ── FILA 1: Combos ──
                int y1 = 12;

                // Tabla
                lblTabla.AutoSize = true;
                lblTabla.Location = new Point(margenH, y1 + 4);

                cbTablas.Location = new Point(lblTabla.Right + espacioP, y1);
                cbTablas.Size = new Size(180, altoCtrl);

                // Proveedor
                lblProveedorFiltro.AutoSize = true;
                lblProveedorFiltro.Location = new Point(cbTablas.Right + espacioG, y1 + 4);

                cbProveedorFiltro.Location = new Point(lblProveedorFiltro.Right + espacioP, y1);
                cbProveedorFiltro.Size = new Size(180, altoCtrl);

                // ── FILA 2: Fechas ──
                int y2 = y1 + altoCtrl + 10;

                // Desde
                lblDesde.AutoSize = true;
                lblDesde.Location = new Point(margenH, y2 + 4);

                dtDesde.Location = new Point(lblDesde.Right + espacioP, y2);
                dtDesde.Size = new Size(350, altoCtrl);

                // Hasta
                lblHasta.AutoSize = true;
                lblHasta.Location = new Point(dtDesde.Right + espacioG, y2 + 4);

                dtHasta.Location = new Point(lblHasta.Right + espacioP, y2);
                dtHasta.Size = new Size(350, altoCtrl);

                // ← Botón buscar al lado de dtHasta
                btnBuscarU.Location = new Point(dtHasta.Right + espacioP, y2);
                btnBuscarU.Size = new Size(80, altoCtrl);

                pnlBotones.Invalidate();
            }

            private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
            {
                // Solo permitir dígitos, punto decimal y backspace
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                {
                    e.Handled = true;
                    return;
                }

                // Solo un punto decimal
                if (e.KeyChar == '.' && txtMonto.Text.Contains('.'))
                {
                    e.Handled = true;
                }
            }

            private void txtMonto_TextChanged(object sender, EventArgs e)
            {
                string texto = txtMonto.Text;

                // Si está vacío o solo tiene el símbolo, limpiar
                if (string.IsNullOrEmpty(texto) || texto == "$")
                {
                    txtMonto.Text = "";
                    return;
                }

                // Quitar el $ para trabajar con el número puro
                if (texto.StartsWith("$"))
                    texto = texto.Substring(1);

                // Poner el cursor al final sin loop infinito
                txtMonto.TextChanged -= txtMonto_TextChanged;
                txtMonto.Text = "$" + texto;
                txtMonto.SelectionStart = txtMonto.Text.Length;
                txtMonto.TextChanged += txtMonto_TextChanged;
            }
        }
    }
