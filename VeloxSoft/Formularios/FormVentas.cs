using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace VeloxSoft.Formularios
{
    public partial class FormVentas : Form
    {
        // Lista maestra — nunca se toca al filtrar
        private List<object[]> _datosVentas = new List<object[]>();
        private FormDetallesVentas _formDetalle;
        private Button btnCerrarDetalle = new Button(); // ← ya no usa new dentro del constructor
        private bool _controlesListos = false;

        public FormVentas()
        {
            InitializeComponent();
            InicializarColumnaEstado();
            CargarDatosPrueba();
            dgvVentas.CellFormatting += dgvVentas_CellFormatting;
            dgvVentas.CellDoubleClick += dgvVentas_CellDoubleClick;

            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            // Fechas ocultas por defecto
            dtpDesde.Visible = false;
            dtpHasta.Visible = false;
            // pero si quieres ser explícito:
            textBuscarU.Visible = true;
            textBuscarN.Visible = true;
            lblBuscarU.Visible = true;
            lblBuscarN.Visible = true;

            checkFecha.CheckedChanged += checkFecha_CheckedChanged;
            btnFiltrar.Click += btnFiltrar_Click;

            EstilizarBtnFiltrar();

            // Botón cerrar detalle
            btnCerrarDetalle.Text = "✕";
            btnCerrarDetalle.Size = new Size(32, 28);
            btnCerrarDetalle.FlatStyle = FlatStyle.Flat;
            btnCerrarDetalle.FlatAppearance.BorderSize = 0;
            btnCerrarDetalle.BackColor = Color.FromArgb(163, 45, 45);
            btnCerrarDetalle.ForeColor = Color.White;
            btnCerrarDetalle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCerrarDetalle.Visible = false;
            btnCerrarDetalle.Cursor = Cursors.Hand;
            btnCerrarDetalle.Click += (s, e) =>
            {
                if (_formDetalle != null)
                {
                    pnlFormulario.Controls.Remove(_formDetalle);
                    _formDetalle.Dispose();
                    _formDetalle = null;
                }
                btnCerrarDetalle.Visible = false;
            };
            pnlFormulario.Controls.Add(btnCerrarDetalle);

            _controlesListos = true;
            pnlVentas_Resize(this, EventArgs.Empty);
            pnlBD_Resize(this, EventArgs.Empty);
            pnlFormulario_Resize(this, EventArgs.Empty);
        }


        private void CargarDatosPrueba()
        {
            // Guarda en la lista maestra
            _datosVentas.Add(new object[] { "3", "$1,200", "19/05/2025", "999-0001", "Efectivo", "Entregado", "C-01", "Admin" });
            _datosVentas.Add(new object[] { "1", "$350", "19/05/2025", "999-0002", "Tarjeta", "Pendiente", "C-02", "Admin" });
            _datosVentas.Add(new object[] { "5", "$4,750", "18/05/2025", "999-0003", "Transferencia", "Entregado", "C-01", "Juan" });
            _datosVentas.Add(new object[] { "2", "$890", "18/05/2025", "999-0004", "Efectivo", "Pendiente", "C-03", "Juan" });
            _datosVentas.Add(new object[] { "4", "$2,100", "17/05/2025", "999-0005", "Tarjeta", "Entregado", "C-02", "Admin" });

            RenderizarGrid(_datosVentas);
        }


        private void InicializarColumnaEstado()
        {
            dgvVentas.ReadOnly = false;
            foreach (DataGridViewColumn col in dgvVentas.Columns)
                col.ReadOnly = true;

            int index = dgvVentas.Columns["colEstado"].Index;
            dgvVentas.Columns.RemoveAt(index);

            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.HeaderText = "Estado";
            combo.Name = "colEstado";
            combo.Items.Add("Entregado");
            combo.Items.Add("Pendiente");
            combo.DisplayIndex = index;
            combo.ReadOnly = false;
            combo.FlatStyle = FlatStyle.Flat;
            dgvVentas.Columns.Add(combo);
            dgvVentas.DataError += (s, ev) => ev.Cancel = true;
        }

        private void RedondearPanel(Panel p, PaintEventArgs e, int radio)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();

            // Dibujamos el camino de las esquinas
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(p.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(p.Width - radio, p.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, p.Height - radio, radio, radio, 90, 90);
            path.CloseAllFigures();

            // Aplicamos la forma al panel para que sea "físicamente" redondo
            p.Region = new Region(path);

            // Dibuja un borde sutil con el verde #A4D1A5 de tu paleta Fruit Salad
            // Esto hace que el cuadro resalte sobre el fondo verde oscuro
            using (Pen pen = new Pen(ColorTranslator.FromHtml("#A4D1A5"), 2))
            {
                e.Graphics.DrawPath(pen, path);
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

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvVentas.Columns[e.ColumnIndex].Name == "colEstado")
                dgvVentas.BeginEdit(true);
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvVentas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvVentas.Columns[e.ColumnIndex].Name != "colEstado" || e.RowIndex < 0) return;
            string valor = e.Value?.ToString() ?? "";
            if (valor == "Entregado")
            {
                e.CellStyle.BackColor = Color.FromArgb(234, 243, 194);
                e.CellStyle.ForeColor = Color.FromArgb(59, 109, 17);
                e.CellStyle.SelectionBackColor = Color.FromArgb(192, 221, 151);
                e.CellStyle.SelectionForeColor = Color.FromArgb(59, 109, 17);
            }
            else if (valor == "Pendiente")
            {
                e.CellStyle.BackColor = Color.FromArgb(224, 100, 68);
                e.CellStyle.ForeColor = Color.FromArgb(133, 79, 11);
                e.CellStyle.SelectionBackColor = Color.FromArgb(250, 199, 117);
                e.CellStyle.SelectionForeColor = Color.FromArgb(133, 79, 11);
            }
        }


        private void pnlVentas_Resize(object sender, EventArgs e)
        {

            int w = pnlVentas.Width;
            int h = pnlVentas.Height;
            int margen = 12;
            int espacioH = 10;

            int anchoIzq = (int)(w * 0.40) - margen;
            int anchoDer = w - anchoIzq - (margen * 3) - espacioH;

            int altoBotones;

            if (checkFecha.Checked)
                altoBotones = 110; // Más alto para mostrar fechas
            else
                altoBotones = anchoDer < 600 ? 110 : 60;


            // PANEL FORMULARIO
            pnlFormulario.Location = new Point(margen, margen);
            pnlFormulario.Size = new Size(anchoIzq, h - margen * 2);

            // PANEL BOTONES
            pnlBotones.Location = new Point(anchoIzq + margen * 2, margen);
            pnlBotones.Size = new Size(anchoDer, altoBotones);

            // PANEL BD
            int yBD = margen + altoBotones + espacioH;
            pnlBD.Location = new Point(anchoIzq + margen * 2, yBD);
            pnlBD.Size = new Size(anchoDer, h - yBD - margen);

            pnlVentas.Invalidate();

        }

        private void pnlBD_Resize(object sender, EventArgs e)
        {
            int w = pnlBD.Width;
            int h = pnlBD.Height;
            int margen = 10;

            dgvVentas.Location = new Point(margen, margen);
            dgvVentas.Size = new Size(w - margen * 2, h - margen * 2);

            pnlBD.Invalidate();
        }

        private void pnlFormulario_Resize(object sender, EventArgs e)
        {
            int w = pnlFormulario.Width;
            int h = pnlFormulario.Height;
            int margenGral = 40;
            int anchoInput = w - (margenGral * 2);

            int altoBotones = 48;
            int espacioEntreBotones = 10;
            int yFilaBotonesAbajo = h - altoBotones - 40;
            int yFilaBotonGuardar = yFilaBotonesAbajo - altoBotones - espacioEntreBotones;

            pnlFormulario.Invalidate();
        }

        private void RenderizarGrid(List<object[]> filas)
        {
            dgvVentas.Rows.Clear();
            foreach (var fila in filas)
                dgvVentas.Rows.Add(fila);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string buscarUsuario = textBuscarU.Text.Trim();
            string buscarNumero = textBuscarN.Text.Trim();

            var filtradas = _datosVentas.AsEnumerable();

            // Filtro por usuario (índice 6)
            if (!string.IsNullOrEmpty(buscarUsuario))
                filtradas = filtradas.Where(f =>
                    f[6].ToString()!.Contains(buscarUsuario, StringComparison.OrdinalIgnoreCase));

            // Filtro por número (índice 0)
            if (!string.IsNullOrEmpty(buscarNumero))
                filtradas = filtradas.Where(f =>
                    f[0].ToString()!.Contains(buscarNumero, StringComparison.OrdinalIgnoreCase));

            // Filtro por fecha solo si el checkbox está marcado
            if (checkFecha.Checked)
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date;

                if (desde > hasta)
                {
                    MessageBox.Show("La fecha 'Desde' no puede ser mayor que 'Hasta'.",
                                    "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                filtradas = filtradas.Where(f =>
                {
                    if (DateTime.TryParseExact(f[7].ToString(), "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime fecha))
                        return fecha >= desde && fecha <= hasta;
                    return false;
                });
            }

            var resultado = filtradas.ToList();
            RenderizarGrid(resultado);

            if (resultado.Count == 0)
                MessageBox.Show("No se encontraron resultados.",
                                "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EstilizarBtnFiltrar()
        {
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.BackColor = Color.FromArgb(59, 109, 17);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFiltrar.Text = "Buscar";
            btnFiltrar.Cursor = Cursors.Hand;
        }

        private void pnlBotones_Resize(object sender, EventArgs e)
        {
            if (!_controlesListos) return;

            int w = pnlBotones.Width;
            int h = pnlBotones.Height;

            int margenH = 20;
            int altoControl = 30;

            int espacioPequeno = 6;
            int espacioGrande = 25;

            bool mostrarFechas = checkFecha.Checked;

            // Labels dinámicos
            Label lblDesde = ObtenerLabel("lblDesde", "Desde:");
            Label lblHasta = ObtenerLabel("lblHasta", "Hasta:");

            // =========================================
            // SIN FECHAS → UNA SOLA FILA
            // =========================================

            if (!mostrarFechas)
            {
                int y = (h - altoControl) / 2;

                int anchoLblUsuario = lblBuscarU.PreferredWidth;
                int anchoLblNumero = lblBuscarN.PreferredWidth;
                int anchoBtn = 100;

                int espacioDisponible =
                    w
                    - (margenH * 2)
                    - anchoLblUsuario
                    - anchoLblNumero
                    - anchoBtn
                    - (espacioPequeno * 4)
                    - espacioGrande;

                int anchoText = espacioDisponible / 2;

                // =========================
                // Usuario
                // =========================

                lblBuscarU.Location = new Point(margenH, y + 5);

                textBuscarU.Location = new Point(
                    lblBuscarU.Right + espacioPequeno,
                    y);

                textBuscarU.Size = new Size(160, altoControl);

                // =========================
                // Número
                // =========================

                lblBuscarN.Location = new Point(
                    textBuscarU.Right + espacioGrande,
                    y + 5);

                textBuscarN.Location = new Point(
                    lblBuscarN.Right + espacioPequeno,
                    y);

                textBuscarN.Size = new Size(140, altoControl);

                // =========================
                // Botón
                // =========================

                btnFiltrar.Location = new Point(
                    w - anchoBtn - margenH,
                    y);

                btnFiltrar.Size = new Size(anchoBtn, altoControl);

                // CheckBox
                checkFecha.Location = new Point(
                    btnFiltrar.Left - 170,
                    y + 5);

                checkFecha.AutoSize = true;

                // Fechas ocultas
                lblDesde.Visible = false;
                lblHasta.Visible = false;

                dtpDesde.Visible = false;
                dtpHasta.Visible = false;
            }

            // =========================================
            // CON FECHAS → DOS FILAS
            // =========================================

            else
            {
                int margenV = 12;

                int anchoDisponible = w - (margenH * 2);

                // =====================================
                // FILA 1
                // =====================================

                int y1 = margenV;

                int anchoLblUsuario = lblBuscarU.PreferredWidth;
                int anchoLblNumero = lblBuscarN.PreferredWidth;
                int anchoBtn = 100;

                int espacioDisponible =
                    anchoDisponible
                    - anchoLblUsuario
                    - anchoLblNumero
                    - anchoBtn
                    - (espacioPequeno * 4)
                    - espacioGrande;

                int anchoText = espacioDisponible / 2;

                // Usuario
                lblBuscarU.Location = new Point(margenH, y1 + 5);

                textBuscarU.Location = new Point(
                    lblBuscarU.Right + espacioPequeno,
                    y1);

                textBuscarU.Size = new Size(160, altoControl);

                // Número
                lblBuscarN.Location = new Point(
                    textBuscarU.Right + espacioGrande,
                    y1 + 5);

                textBuscarN.Location = new Point(
                    lblBuscarN.Right + espacioPequeno,
                    y1);

                textBuscarN.Size = new Size(140, altoControl);

                // Botón
                btnFiltrar.Location = new Point(
                    w - anchoBtn - margenH,
                    y1);

                btnFiltrar.Size = new Size(anchoBtn, altoControl);

                // CheckBox
                checkFecha.Location = new Point(
                    btnFiltrar.Left - 170,
                    y1 + 5);

                checkFecha.AutoSize = true;



                // =====================================
                // FILA 2
                // =====================================

                int y2 = y1 + altoControl + 15;

                lblDesde.Visible = true;
                lblHasta.Visible = true;

                dtpDesde.Visible = true;
                dtpHasta.Visible = true;

                int anchoGrupoFecha =
                    (anchoDisponible - espacioGrande) / 2;

                // DESDE
                lblDesde.Location = new Point(
                    margenH,
                    y2 + 5);

                dtpDesde.Location = new Point(
                    lblDesde.Right + espacioPequeno,
                    y2);

                dtpDesde.Size = new Size(
                    anchoGrupoFecha - lblDesde.Width - espacioPequeno,
                    altoControl);

                // HASTA
                lblHasta.Location = new Point(
                    dtpDesde.Right + espacioGrande,
                    y2 + 5);

                dtpHasta.Location = new Point(
                    lblHasta.Right + espacioPequeno,
                    y2);

                dtpHasta.Size = new Size(
                    w - dtpHasta.Left - margenH,
                    altoControl);
            }

            pnlBotones.Invalidate();
        }


        private Label ObtenerLabel(string nombre, string texto)
        {
            foreach (Control c in pnlBotones.Controls)
                if (c.Name == nombre) return (Label)c;

            Label lbl = new Label();
            lbl.Name = nombre;
            lbl.Text = texto;
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(59, 109, 17);
            pnlBotones.Controls.Add(lbl);
            return lbl;
        }

        private void dgvVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgvVentas.Rows[e.RowIndex];
            string idVenta = fila.Cells["colCantidad"].Value?.ToString() ?? "";
            string fecha = fila.Cells["colFecha"].Value?.ToString() ?? "";
            string telefono = fila.Cells["colTelefono"].Value?.ToString() ?? "";

            if (_formDetalle != null)
            {
                pnlFormulario.Controls.Remove(_formDetalle);
                _formDetalle.Dispose();
            }

            _formDetalle = new FormDetallesVentas(idVenta, telefono, fecha);
            _formDetalle.TopLevel = false;
            _formDetalle.FormBorderStyle = FormBorderStyle.None;
            _formDetalle.Dock = DockStyle.Fill;
            _formDetalle.Visible = true;
            pnlFormulario.Controls.Add(_formDetalle);
            _formDetalle.BringToFront();

            btnCerrarDetalle.Location = new Point(pnlFormulario.Width - 38, 4);
            btnCerrarDetalle.Visible = true;
            btnCerrarDetalle.BringToFront();
        }

        private void checkFecha_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = checkFecha.Checked;

            // Mostrar fechas
            dtpDesde.Visible = mostrar;
            dtpHasta.Visible = mostrar;

            // LOS BUSCADORES SIEMPRE VISIBLES
            textBuscarU.Visible = true;
            textBuscarN.Visible = true;
            lblBuscarU.Visible = true;
            lblBuscarN.Visible = true;

            // Redibujar panel
            pnlBotones_Resize(this, EventArgs.Empty);
            pnlVentas_Resize(this, EventArgs.Empty);
        }

        private void RedondearBoton(Button btn, PaintEventArgs e, int radio)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Hace que el borde se vea suave

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radio, radio, 180, 90);
                path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
                path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
                path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
                path.CloseAllFigures();

                // Aplicamos la región redondeada al botón
                btn.Region = new Region(path);
            }
        }

        private void btnFiltrar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnFiltrar, e, 15);
        }
    }
}
