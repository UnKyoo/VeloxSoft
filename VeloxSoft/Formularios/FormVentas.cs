using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using VeloxSoft.Models;
using VeloxSoft.Services;


namespace VeloxSoft.Formularios
{
    public partial class FormVentas : Form
    {
        private readonly ServicioVentas _ServicioVentas;
        private bool _cargando = false;
        private bool _revirtiendo = false;
        // Lista maestra — nunca se toca al filtrar
        private FormDetallesVentas _formDetalle;
        private Button btnCerrarDetalle = new Button(); // ← ya no usa new dentro del constructor
        private bool _controlesListos = false;

        public FormVentas(ServicioVentas servicioVentas)
        {
            _ServicioVentas = servicioVentas;
            InitializeComponent();
            InicializarColumnaEstado();
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
            lblNVenta.Visible = true;
            lblNCliente.Visible = true;

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

        // Logica de funcionamiento

        private void CargarVentas()
        {
            _cargando = true;
            List<Venta> Ventas = _ServicioVentas.Buscar_Ventas(
                textBuscarU.Text,   // NVenta
                textBuscarN.Text,   // NCliente
                cmbEstado.Text,     // Estado
                checkFecha.Checked ? dtpDesde.Value.ToString("yyyy-MM-dd") : null,
                checkFecha.Checked ? dtpHasta.Value.ToString("yyyy-MM-dd") : null,
                out string errorMessage
            );
            dgvVentas.Rows.Clear();

            foreach (var Venta in Ventas)
            {
                int index = dgvVentas.Rows.Add(
                    Venta.IdVenta,
                    Venta.NumCel,
                    Venta.Cantidad.ToString("F3"),
                    $"${Venta.Importe.ToString("F2")}",
                    Venta.TipoDePago,
                    Venta.IdCorte?.ToString() ?? "Sin corte",
                    Venta.IdUsuario,
                    Venta.Fecha.ToString("yyyy-MM-dd")
                );

                DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dgvVentas.Rows[index].Cells["colEstado"];
                comboCell.Value = Venta.Estado;
                dgvVentas.Rows[index].Tag = Venta.Estado;

                if (Venta.Estado == "Entregado")
                    dgvVentas.Rows[index].ReadOnly = true;
            }

            _cargando = false;
        }


        private void FormVentas_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }
        private void dgvVentas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentCell?.OwningColumn.Name == "colEstado")
                dgvVentas.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgvVentas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cargando || _revirtiendo) return;

            if (dgvVentas.Columns[e.ColumnIndex].Name == "colEstado" && e.RowIndex >= 0)
            {
                string nuevoEstado = dgvVentas.Rows[e.RowIndex].Cells["colEstado"].Value?.ToString();
                string estadoAnterior = dgvVentas.Rows[e.RowIndex].Tag?.ToString(); // guardamos el anterior
                long idVenta = (long)dgvVentas.Rows[e.RowIndex].Cells["colVenta"].Value;

                var confirmacion = MessageBox.Show(
                    $" Venta: {idVenta} \n ¿Deseas cambiar el estado a '{nuevoEstado}'?",
                    "Confirmar cambio",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    string idEstado = nuevoEstado switch
                    {
                        "Entregado" => "EN",
                        "Pendiente" => "PE",
                        "Cancelado" => "CA",
                        _ => null
                    };

                    bool exito = _ServicioVentas.Actualizar_Estado(idVenta, idEstado, out string errorMessage);

                    if (exito)
                    {
                        // Guardar el nuevo estado como anterior
                        dgvVentas.Rows[e.RowIndex].Tag = nuevoEstado;

                        if (nuevoEstado == "Entregado" || nuevoEstado == "Cancelado")
                            dgvVentas.Rows[e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        _revirtiendo = true;
                        dgvVentas.Rows[e.RowIndex].Cells["colEstado"].Value = estadoAnterior;
                        _revirtiendo = false;
                    }
                }
                else
                {
                    _revirtiendo = true;
                    dgvVentas.Rows[e.RowIndex].Cells["colEstado"].Value = estadoAnterior;
                    _revirtiendo = false;
                }
            }
        }

        private void textBuscarU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void textBuscarN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        // Fin 

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
            combo.Items.Add("Cancelado");
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
                e.CellStyle.BackColor = Color.FromArgb(220, 220, 220);
                e.CellStyle.ForeColor = Color.FromArgb(90, 90, 90);
                e.CellStyle.SelectionBackColor = Color.FromArgb(180, 180, 180);
                e.CellStyle.SelectionForeColor = Color.FromArgb(90, 90, 90);
            }
            else if (valor == "Cancelado")
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
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarVentas();
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
            int espacioPequeno = 0;
            int espacioGrande = 8;

            bool mostrarFechas = checkFecha.Checked;

            Label lblDesde = ObtenerLabel("lblDesde", "Desde:");
            Label lblHasta = ObtenerLabel("lblHasta", "Hasta:");

            Label lblNVenta = ObtenerLabel("lblNVenta", "N° Venta:");
            Label lblNCliente = ObtenerLabel("lblNCliente", "N° Cliente:");
            Label lblEstado = ObtenerLabel("lblEstado", "Estado:");

            ComboBox cmbEstado = (ComboBox)pnlBotones.Controls["cmbEstado"];

            // =========================================
            // SIN FECHAS → UNA SOLA FILA
            // =========================================
            if (!mostrarFechas)
            {
                int y = (h - altoControl) / 2;

                // N° Venta
                lblNVenta.Location = new Point(margenH, y + 5);
                textBuscarU.Location = new Point(lblNVenta.Right + espacioPequeno, y);
                textBuscarU.Size = new Size(180, altoControl);

                // N° Cliente
                lblNCliente.Location = new Point(textBuscarU.Right + espacioGrande, y + 5);
                textBuscarN.Location = new Point(lblNCliente.Right + espacioPequeno, y);
                textBuscarN.Size = new Size(130, altoControl);

                // Estado
                lblEstado.Location = new Point(textBuscarN.Right + espacioGrande, y + 5);
                cmbEstado.Location = new Point(lblEstado.Right + espacioPequeno, y);
                cmbEstado.Size = new Size(110, altoControl);

                // Botón
                btnFiltrar.Location = new Point(w - 100 - margenH, y);
                btnFiltrar.Size = new Size(100, altoControl);

                // CheckBox
                checkFecha.AutoSize = true;
                checkFecha.Location = new Point(
                    btnFiltrar.Left - checkFecha.PreferredSize.Width - espacioPequeno,
                    y + 5
                );

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

                // N° Venta
                lblNVenta.Location = new Point(margenH, y1 + 5);
                textBuscarU.Location = new Point(lblNVenta.Right + espacioPequeno, y1);
                textBuscarU.Size = new Size(180, altoControl);

                // N° Cliente
                lblNCliente.Location = new Point(textBuscarU.Right + espacioGrande, y1 + 5);
                textBuscarN.Location = new Point(lblNCliente.Right + espacioPequeno, y1);
                textBuscarN.Size = new Size(130, altoControl);

                // Estado
                lblEstado.Location = new Point(textBuscarN.Right + espacioGrande, y1 + 5);
                cmbEstado.Location = new Point(lblEstado.Right + espacioPequeno, y1);
                cmbEstado.Size = new Size(110, altoControl);

                // Botón
                btnFiltrar.Location = new Point(w - 100 - margenH, y1);
                btnFiltrar.Size = new Size(100, altoControl);

                // CheckBox
                checkFecha.AutoSize = true;
                checkFecha.Location = new Point(
                    btnFiltrar.Left - checkFecha.PreferredSize.Width - espacioPequeno,
                    y1 + 5
                );

                // =====================================
                // FILA 2
                // =====================================
                int y2 = y1 + altoControl + 15;

                lblDesde.Visible = true;
                lblHasta.Visible = true;
                dtpDesde.Visible = true;
                dtpHasta.Visible = true;

                int anchoGrupoFecha = (anchoDisponible - espacioGrande) / 2;

                lblDesde.Location = new Point(margenH, y2 + 5);
                dtpDesde.Location = new Point(lblDesde.Right + espacioPequeno, y2);
                dtpDesde.Size = new Size(anchoGrupoFecha - lblDesde.Width - espacioPequeno, altoControl);

                lblHasta.Location = new Point(dtpDesde.Right + espacioGrande, y2 + 5);
                dtpHasta.Location = new Point(lblHasta.Right + espacioPequeno, y2);
                dtpHasta.Size = new Size(w - dtpHasta.Left - margenH, altoControl);
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
            string idVenta = fila.Cells["colVenta"].Value?.ToString() ?? "";
            string fecha = fila.Cells["colFecha"].Value?.ToString() ?? "";
            string telefono = fila.Cells["colTelefono"].Value?.ToString() ?? "";

            if (_formDetalle != null)
            {
                pnlFormulario.Controls.Remove(_formDetalle);
                _formDetalle.Dispose();
            }

            _formDetalle = new FormDetallesVentas(idVenta, telefono, fecha, _ServicioVentas);
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
            lblNVenta.Visible = true;
            lblNCliente.Visible = true;

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
