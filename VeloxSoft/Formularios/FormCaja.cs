using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using VeloxSoft.Models;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormCaja : Form
    {
        private List<ProductoCarrito> _carrito = new List<ProductoCarrito>();
        private Producto? _productoSeleccionado;
        private Usuario? _usuarioSeleccionado;
        private readonly ServicioCaja _ServicioCaja;

        // Datos de prueba productos
        

        public FormCaja(ServicioCaja servicioCaja)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            pnlCaja_Resize(this, EventArgs.Empty);
            pnlFormulario_Resize(this, EventArgs.Empty);
            // Checkbox siempre fijo arriba
            checkCaja.Location = new Point(26, 17);

            // Estado inicial sin domicilio
            lblUsuario.Visible = false;
            txtUsuario.Visible = false;
            lstSugerencias2.Visible = false;

            MoverAModoSinDomicilio();

            checkCaja.CheckedChanged += checkCaja_CheckedChanged;
            pnlCaja_Resize(this, EventArgs.Empty);
            pnlFormulario_Resize(this, EventArgs.Empty);

            /*var metodo = cbMetodoPago.SelectedItem as MetodoPago;
            string idPago = metodo?.Id ?? "EF"; // Efectivo por defecto*/
            _ServicioCaja = servicioCaja;
            CargarMetodosPago();
        }

        // ── BÚSQUEDA PRODUCTO ────────────────────────────────────

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                lstSugerencias.Visible = false;
                _productoSeleccionado = null;
                return;
            }

            var resultados = _ServicioCaja.BuscarProductos(texto, out string _);

            lstSugerencias.Items.Clear();

            if (resultados.Count > 0)
            {
                foreach (var p in resultados)
                    lstSugerencias.Items.Add(p); // usa ToString() del modelo

                lstSugerencias.Tag = resultados;
                lstSugerencias.Visible = true;
            }
            else
            {
                lstSugerencias.Visible = false;
            }
        }

        private void lstSugerencias_Click(object sender, EventArgs e)
        {
            if (lstSugerencias.SelectedIndex < 0) return;

            var lista = lstSugerencias.Tag as List<Producto>;
            if (lista == null) return;

            _productoSeleccionado = lista[lstSugerencias.SelectedIndex];

            // Validación: stock suficiente para al menos 1 unidad
            if (_productoSeleccionado.Cantidad <= 0)
            {
                MostrarError("Este producto no tiene stock disponible.");
                _productoSeleccionado = null;
                return;
            }

            txtBuscar.Text = _productoSeleccionado.Nombre;
            nudCantidad.Maximum = (decimal)_productoSeleccionado.Cantidad; // límite de stock
            nudCantidad.Value = 1;
            lblUnidad.Text = _productoSeleccionado.IdCategoria;
            lstSugerencias.Visible = false;
        }

        // ── BÚSQUEDA USUARIO ─────────────────────────────────────

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (!checkCaja.Checked) return;

            string texto = txtUsuario.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                lstSugerencias2.Visible = false;
                _usuarioSeleccionado = null;
                return;
            }

            var resultados = _ServicioCaja.BuscarUsuarios(texto, out string _);

            lstSugerencias2.Items.Clear();

            if (resultados.Count > 0)
            {
                foreach (var u in resultados)
                    lstSugerencias2.Items.Add(u); // usa ToString() del modelo

                lstSugerencias2.Tag = resultados;
                lstSugerencias2.Visible = true;
            }
            else
            {
                lstSugerencias2.Visible = false;
            }
        }

        private void lstSugerencias2_Click(object sender, EventArgs e)
        {
            if (lstSugerencias2.SelectedIndex < 0) return;

            var lista = lstSugerencias2.Tag as List<Usuario>;
            if (lista == null) return;

            _usuarioSeleccionado = lista[lstSugerencias2.SelectedIndex];
            txtUsuario.Text = $"{_usuarioSeleccionado.Nombre} ({_usuarioSeleccionado.Id})";
            lstSugerencias2.Visible = false;

            // Bloquear campo
            txtUsuario.ReadOnly = true;
            txtUsuario.BackColor = Color.FromArgb(220, 220, 220);

            // Mostrar info en el recuadro si tienes pnlInfoUsuario
            lblInfoNombre.Text = _usuarioSeleccionado.Nombre;
            lblInfoNombre.Visible = true;
            lblInfoId.Text = $"Tel: {_usuarioSeleccionado.Id}";
            lblInfoId.Visible = true;
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            // Si presiona Delete o Backspace estando bloqueado, desbloquea
            if (txtUsuario.ReadOnly &&
               (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back))
            {
                txtUsuario.ReadOnly = false;
                txtUsuario.BackColor = Color.FromArgb(250, 254, 247);
                txtUsuario.Clear();
                _usuarioSeleccionado = null;
                lblInfoNombre.Text = "";
                lblInfoId.Text = "";
                e.Handled = true;
            }
        }

        private void CargarMetodosPago()
        {
            var lista = _ServicioCaja.Ver_MetodosPago(out string _);//out strin _ para ignorar mensaje

            cbMetodoPago.Items.Clear();
            foreach (var (id, tipo) in lista)
                cbMetodoPago.Items.Add(new MetodoPago { Id = id, Tipo = tipo });

            if (cbMetodoPago.Items.Count > 0)
                cbMetodoPago.SelectedIndex = 0;
        }

        // ── CARRITO ──────────────────────────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            lblErrores.Visible = false;

            if (_productoSeleccionado == null)
            {
                MostrarError("Selecciona un producto primero.");
                return;
            }

            decimal cantidad = nudCantidad.Value;

            if (cantidad > _productoSeleccionado.Cantidad)
            {
                MostrarError($"Stock insuficiente. Disponible: {_productoSeleccionado.Cantidad:N2} {_productoSeleccionado.IdCategoria}");
                return;
            }

            var existente = _carrito.FirstOrDefault(c => c.Id == _productoSeleccionado.IdProducto);

            if (existente != null)
            {
                if (existente.Cantidad + cantidad > _productoSeleccionado.Cantidad)
                {
                    MostrarError($"No puedes agregar más. Stock disponible: {_productoSeleccionado.Cantidad:N2} {_productoSeleccionado.IdCategoria}");
                    return;
                }
                existente.Cantidad += cantidad;
            }
            else
            {
                _carrito.Add(new ProductoCarrito
                {
                    Id = _productoSeleccionado.IdProducto,
                    Nombre = _productoSeleccionado.Nombre,
                    Precio = _productoSeleccionado.Precio,
                    Unidad = _productoSeleccionado.IdCategoria,
                    Cantidad = cantidad
                });
            }

            RenderizarCarrito();
            LimpiarBusqueda();
        }

        private void dgvCarrito_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCarrito.Columns[e.ColumnIndex].Name == "colEliminar")
            {
                string id = dgvCarrito.Rows[e.RowIndex].Cells["colId"].Value?.ToString() ?? "";
                _carrito.RemoveAll(c => c.Id == id);
                RenderizarCarrito();
            }
        }

        private void dgvCarrito_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCarrito.Columns[e.ColumnIndex].Name != "colCantidad") return;

            string id = dgvCarrito.Rows[e.RowIndex].Cells["colId"].Value?.ToString() ?? "";
            string val = dgvCarrito.Rows[e.RowIndex].Cells["colCantidad"].Value?.ToString() ?? "0";

            if (decimal.TryParse(val, out decimal nuevaCantidad) && nuevaCantidad > 0)
            {
                var item = _carrito.FirstOrDefault(c => c.Id == id);
                if (item != null)
                {
                    item.Cantidad = nuevaCantidad;
                    RenderizarCarrito();
                }
            }
            else
            {
                MessageBox.Show("Cantidad inválida. Debe ser mayor a 0.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RenderizarCarrito();
            }
        }

        private void btnTerminarCompra_Click(object sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.",
                    "Sin productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string usuario = _usuarioSeleccionado?.Nombre ?? "Sin usuario";
            string metodo = cbMetodoPago.SelectedItem?.ToString() ?? "Efectivo";
            decimal total = _carrito.Sum(c => c.Subtotal);

            string resumen = string.Join("\n", _carrito.Select(c =>
                $"  {c.Nombre} x{c.Cantidad} {c.Unidad} = ${c.Subtotal:F2}"));

            var resultado = MessageBox.Show(
                $"Cliente: {usuario}\n\nProductos:\n{resumen}\n\nTotal: ${total:F2}\nPago: {metodo}\n\n¿Confirmar venta?",
                "Confirmar compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Aquí guardarás en BD cuando conectes
                MessageBox.Show("¡Venta guardada correctamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTodo();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Limpiar todo el carrito?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
                LimpiarTodo();
        }

        // ── HELPERS ──────────────────────────────────────────────

        private void RenderizarCarrito()
        {
            dgvCarrito.Rows.Clear();
            foreach (var item in _carrito)
            {
                dgvCarrito.Rows.Add(
                    item.Id,
                    item.Nombre,
                    $"${item.Precio:F2}",
                    item.Unidad,
                    item.Cantidad.ToString("F2"),
                    $"${item.Subtotal:F2}"
                );
            }
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total = _carrito.Sum(c => c.Subtotal);
            lblTotalValor.Text = $"${total:F2}";
        }
        private void MostrarError(string mensaje)
        {
            lblErrores.Text = mensaje;
            lblErrores.ForeColor = Color.FromArgb(163, 45, 45);
            lblErrores.Visible = true;
        }
        private void LimpiarBusqueda()
        {
            txtBuscar.Clear();
            nudCantidad.Value = 1;
            lblUnidad.Text = "pza";
            _productoSeleccionado = null;
            lstSugerencias.Visible = false;
        }

        private void LimpiarTodo()
        {
            _carrito.Clear();
            RenderizarCarrito();
            LimpiarBusqueda();
            txtUsuario.Clear();
            _usuarioSeleccionado = null;
            lstSugerencias2.Visible = false;
            cbMetodoPago.SelectedIndex = 0;
            txtUsuario.ReadOnly = false;
            txtUsuario.BackColor = Color.FromArgb(250, 254, 247);
            lblInfoNombre.Visible = false;
            lblInfoId.Visible = false;
        }

        // ── DISEÑO ───────────────────────────────────────────────

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
                e.Graphics.DrawPath(pen, path);
        }

        private void pnlFormulario_Paint(object sender, PaintEventArgs e) => RedondearPanel((Panel)sender, e, 15);
        private void pnlBotones_Paint(object sender, PaintEventArgs e) => RedondearPanel((Panel)sender, e, 15);
        private void pnlBD_Paint(object sender, PaintEventArgs e) => RedondearPanel((Panel)sender, e, 15);

        // ── RESIZE ───────────────────────────────────────────────

        private void pnlCaja_Resize(object sender, EventArgs e)
        {
            int w = pnlCaja.Width, h = pnlCaja.Height;
            int margen = 12, espacioH = 10, altoBotones = 90;
            int anchoIzq = (int)(w * 0.35) - margen;
            int anchoDer = w - anchoIzq - (margen * 3) - espacioH;

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
            int anchoCtrl = w - 46;

            txtUsuario.Size = new Size(anchoCtrl, 32);
            lstSugerencias2.Size = new Size(anchoCtrl, 117);
            txtBuscar.Size = new Size(anchoCtrl, 32);
            lstSugerencias.Size = new Size(anchoCtrl, 117);
            nudCantidad.Size = new Size(anchoCtrl - 60, 32);
            lblUnidad.Location = new Point(nudCantidad.Right + 8, nudCantidad.Top + 4);
            btnAgregar.Size = new Size(anchoCtrl, 42);
            cbMetodoPago.Size = new Size(anchoCtrl, 32);
            btnTerminarCompra.Size = new Size(anchoCtrl, 50);
            btnLimpiar.Size = new Size(anchoCtrl, 42);

            pnlFormulario.Invalidate();
        }

        private void pnlBotones_Resize(object sender, EventArgs e)
        {
            pnlBotones.Invalidate();
        }

        // ── EVENTOS VACÍOS REQUERIDOS POR DESIGNER ───────────────
        private void label1_Click(object sender, EventArgs e) { }

        private void checkCaja_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = checkCaja.Checked;

            lblUsuario.Visible = mostrar;
            txtUsuario.Visible = mostrar;
            lstSugerencias2.Visible = false;

            if (mostrar)
                MoverAModoDomicilio();
            else
                MoverAModoSinDomicilio();

            if (!mostrar)
            {
                _usuarioSeleccionado = null;
                txtUsuario.Clear();
            }
        }

        private void MoverAModoSinDomicilio()
        {
            // Checkbox fijo
            checkCaja.Location = new Point(26, 17);

            // Producto justo debajo del checkbox
            lblBuscar.Location = new Point(26, 55);
            txtBuscar.Location = new Point(26, 83);
            lstSugerencias.Location = new Point(26, 121);

            // Cantidad y agregar
            lblCantidad.Location = new Point(26, 250);
            nudCantidad.Location = new Point(26, 278);
            lblUnidad.Location = new Point(nudCantidad.Right + 8, 282);
            btnAgregar.Location = new Point(26, 328);

            // Método de pago
            lblMetodoPago.Location = new Point(26, 398);
            cbMetodoPago.Location = new Point(26, 426);

            // Total y botones
            lblTotal.Location = new Point(26, 510);
            lblTotalValor.Location = new Point(26, 545);
            btnTerminarCompra.Location = new Point(26, 640);
            btnLimpiar.Location = new Point(26, 700);
        }

        private void MoverAModoDomicilio()
        {
            // Checkbox fijo
            checkCaja.Location = new Point(26, 17);

            // Usuario debajo del checkbox
            lblUsuario.Location = new Point(26, 55);
            txtUsuario.Location = new Point(26, 83);
            lstSugerencias2.Location = new Point(26, 121);
                
            // Producto debajo del usuario
            lblBuscar.Location = new Point(26, 255);
            txtBuscar.Location = new Point(26, 283);
            lstSugerencias.Location = new Point(26, 321);

            // Cantidad y agregar
            lblCantidad.Location = new Point(26, 455);
            nudCantidad.Location = new Point(26, 483);
            lblUnidad.Location = new Point(nudCantidad.Right + 8, 487);
            btnAgregar.Location = new Point(26, 533);

            // Método de pago
            lblMetodoPago.Location = new Point(26, 603);
            cbMetodoPago.Location = new Point(26, 631);

            // Total y botones
            lblTotal.Location = new Point(26, 700);
            lblTotalValor.Location = new Point(26, 735);
            btnTerminarCompra.Location = new Point(26, 830);
            btnLimpiar.Location = new Point(26, 890);
        }

    }
}