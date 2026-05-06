using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormInventario : Form
    {
        private bool _modoEdicion = false; // Variable para controlar si estamos editando o creando un nuevo producto
        private readonly ServicioInventario _ServicioInventario;
        public FormInventario(ServicioInventario ServicioInventario)
        {
            InitializeComponent();
            _ServicioInventario = ServicioInventario;
            // Esto obliga al formulario a redibujarse mientras el usuario arrastra el mouse
            this.ResizeRedraw = true;
            // Evita el efecto de "congelado" o parpadeo
            this.DoubleBuffered = true;
            pnlBD_Resize(this, EventArgs.Empty);

        }

        //BORDES REDONDOS PARA LOS PANEL, SOLO LO LLAMAMOS EN LOS EVENTOS PAINT DE LOS PANEL, ASÍ SE DIBUJAN LOS BORDES CUANDO SE REDIMENSIONA LA VENTANA
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


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlDetalles_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void textID_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }



        private void pnlNombre_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlPC_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void pnlStock_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textPV_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlPV_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un producto en la tabla
            if (dtgBDInv.SelectedRows.Count == 0)
            {
                LabelError.Text = "Debe seleccionar un producto para eliminar.";
                LabelError.Visible = true;
                LabelError.ForeColor = Color.Red;
                return;
            }

            // Obtener el ID del producto seleccionado en la tabla
            string idProducto = dtgBDInv.SelectedRows[0].Cells[0].Value.ToString(); // O el nombre de la columna que corresponda

            // Mostrar un cuadro de diálogo de confirmación antes de eliminar el producto
            var Evitemos_Errores = MessageBox.Show(
                $"¿Está seguro de que desea eliminar el producto con ID: {idProducto}?", //confirmación de eliminación, se muestra el ID del producto para que el usuario pueda verificar que está eliminando el producto correcto
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            // Si el usuario selecciona "No", se cancela la operación de eliminación
            if (Evitemos_Errores == DialogResult.No) return;

            //Llamar al serivico para eliminar el producto.
            _ServicioInventario.Eliminar_Producto(idProducto, out string errorMessage);

            //Mostrar mensaje de error o éxito dependiendo del resultado de la operación
            if (!string.IsNullOrEmpty(errorMessage))
            {
                LabelError.Text = errorMessage;
                LabelError.Visible = true;
            }
            else
            {
                LabelError.Text = "Producto eliminado correctamente.";
                LabelError.ForeColor = Color.Green;
                LabelError.Visible = true;
                CargarInventario();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!_modoEdicion)
            {
                // 1. Validar que los campos no estén vacíos
                if (string.IsNullOrWhiteSpace(textID.Text) ||
                    string.IsNullOrWhiteSpace(textNombre.Text) ||
                    string.IsNullOrWhiteSpace(textPV.Text) ||
                    string.IsNullOrWhiteSpace(textStock.Text))
                {
                    LabelError2.Text = "Todos los campos son obligatorios.";
                    LabelError2.Visible = true;
                    LabelError2.ForeColor = Color.Red;
                    return;
                }

                // 2. Validar que precio y cantidad sean números
                if (!decimal.TryParse(textPV.Text, out decimal precio))
                {
                    LabelError2.Text = "El precio debe ser un número válido.";
                    LabelError2.ForeColor = Color.Red;
                    return;
                }
                if (!decimal.TryParse(textStock.Text, out decimal cantidad))
                {
                    LabelError2.Text = "El stock debe ser un número válido.";
                    LabelError2.ForeColor = Color.Red;
                    return;
                }

                // 3. Obtener categoría del ComboBox
                if (BoxPrueba.SelectedItem == null)
                {
                    LabelError2.Text = "Debe seleccionar una categoría.";
                    LabelError2.ForeColor = Color.Red;
                    LabelError2.Visible = true;
                    return;
                }

            string categoria = BoxPrueba.SelectedItem.ToString();

            if (categoria == "Pieza")
            {
                categoria = "PZ";
            }
            else if (categoria == "Kilo")
            {
                categoria = "KG";
            }

            // DEBUG - borrar después
            MessageBox.Show($"ID: {textID.Text.Trim()}\nNombre: {lblNombre.Text.Trim()}\nCantidad: {cantidad}\nPrecio: {precio}\nCategoria: {categoria}");

                // 4. Llamar al servicio para insertar el producto
                string mensaje = _ServicioInventario.Insertar_Producto(textID.Text.Trim(), textNombre.Text.Trim(), cantidad, precio, categoria, out string errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    LabelError2.Text = errorMessage;
                    LabelError2.Visible = true;
                    LabelError2.ForeColor = Color.Red;
                    CargarInventario();
                    return;
                }
                else
                {

                    LabelError2.Text = mensaje; // "Producto agregado correctamente."
                    LabelError2.Visible = true;
                    LabelError2.ForeColor = Color.Green;
                    CargarInventario(); // Recarga la tabla para mostrar el nuevo producto

                    //Limpiamos los campos después de guardar
                    textID.Text = string.Empty;
                    textNombre.Text = string.Empty;
                    textPV.Text = string.Empty;
                    textStock.Text = string.Empty;
                    BoxPrueba.SelectedItem = null;
                }
            }
            else 
            { 
               
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Limpiamos los campos para permitir ingresar un nuevo producto
            textID.Text = string.Empty;
            textNombre.Text = string.Empty;
            textPV.Text = string.Empty;
            textStock.Text = string.Empty;
            BoxPrueba.Text = "";

            //desbloquear el campo de ID en caso de que estemos editando un producto
            textID.ReadOnly = false;
            textID.Enabled = true;
            textNombre.ReadOnly = false;
            textNombre.Enabled = true;

            //Desactivamos el modo edición para que al guardar se cree un nuevo producto en lugar de actualizar uno existente
            _modoEdicion = false;

            // Limpieza de mensaje de error
            LabelError2.Text = string.Empty;
            LabelError2.Visible = false;
        }

        private void btnNuevo_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnNuevo, e, 15);
        }

        private void btnGuardar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnGuardar, e, 15);
        }

        private void btnEliminar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnEliminar, e, 15);
        }

        private void pnlBD_Resize(object sender, EventArgs e)
        {
            int w = pnlBD.Width;
            int h = pnlBD.Height;
            int margen = 15;
            int altoControl = 32;

            // Umbral — si el panel es mayor a 600px todo en una línea
            bool unaLinea = w >= 600;

            if (unaLinea)
            {
                // ── PANTALLA GRANDE — todo en una sola fila ──
                int y = 15;
                int x = margen;

                int anchoLbl = (int)(w * 0.10);
                int anchoBuscar = (int)(w * 0.20);
                int anchoBtn = (int)(w * 0.06);
                int anchoCmb = (int)(w * 0.14);

                // BUSCAR ID
                BuscarBD.Location = new Point(x, y + 5);
                BuscarBD.Size = new Size(anchoLbl, 22);
                x += anchoLbl + 4;

                pnlBuscarBD.Location = new Point(x, y);
                pnlBuscarBD.Size = new Size(anchoBuscar, altoControl);
                tbBuscarBD.Location = new Point(4, 7);
                tbBuscarBD.Size = new Size(anchoBuscar - 8, 18);
                x += anchoBuscar + 4;

                btnGuardarBD.Location = new Point(x, y);
                btnGuardarBD.Size = new Size(anchoBtn, altoControl);
                x += anchoBtn + 10;

                // CATEGORÍA
                lbCategoria.Location = new Point(x, y + 5);
                lbCategoria.Size = new Size(anchoLbl, 22);
                x += anchoLbl + 4;

                cbCategoria.Location = new Point(x, y);
                cbCategoria.Size = new Size(anchoCmb, altoControl);
                x += anchoCmb + 10;

                // ESTADO
                lbEstado.Location = new Point(x, y + 5);
                lbEstado.Size = new Size(anchoLbl, 22);
                x += anchoLbl + 4;

                cbEstado.Location = new Point(x, y);
                cbEstado.Size = new Size(anchoCmb, altoControl);

                y += altoControl + 8;

                LabelError.Location = new Point(margen, y);
                LabelError.Size = new Size(w - margen * 2, 18);
                y += 22;

                dtgBDInv.Location = new Point(margen, y);
                dtgBDInv.Size = new Size(w - margen * 2, h - y - 8);
            }
            else
            {
                // ── PANTALLA PEQUEÑA — buscar arriba, combos abajo ──
                int anchoBuscar = w - margen * 2 - 50;
                int anchoCmb = (int)((w - margen * 2 - 10) / 2);

                // FILA 1 — BUSCAR ID
                int y1 = 10;
                BuscarBD.Location = new Point(margen, y1 + 5);
                BuscarBD.Size = new Size(80, 22);

                pnlBuscarBD.Location = new Point(margen + 84, y1);
                pnlBuscarBD.Size = new Size(anchoBuscar - 84, altoControl);
                tbBuscarBD.Location = new Point(4, 7);
                tbBuscarBD.Size = new Size(pnlBuscarBD.Width - 8, 18);

                btnGuardarBD.Location = new Point(w - margen - 46, y1);
                btnGuardarBD.Size = new Size(42, altoControl);

                // FILA 2 — COMBOS
                int y2 = y1 + altoControl + 8;

                lbCategoria.Location = new Point(margen, y2 + 5);
                lbCategoria.Size = new Size(75, 22);

                cbCategoria.Location = new Point(margen + 78, y2);
                cbCategoria.Size = new Size(anchoCmb - 78, altoControl);

                lbEstado.Location = new Point(margen + anchoCmb + 10, y2 + 5);
                lbEstado.Size = new Size(55, 22);

                cbEstado.Location = new Point(margen + anchoCmb + 68, y2);
                cbEstado.Size = new Size(anchoCmb - 68, altoControl);

                int y3 = y2 + altoControl + 6;

                LabelError.Location = new Point(margen, y3);
                LabelError.Size = new Size(w - margen * 2, 18);
                y3 += 22;

                dtgBDInv.Location = new Point(margen, y3);
                dtgBDInv.Size = new Size(w - margen * 2, h - y3 - 8);
            }

            pnlBD.Invalidate();
        }

        private void pnlDetalles_Resize(object sender, EventArgs e)
        {
            int w = pnlDetalles.Width;
            int h = pnlDetalles.Height;
            int margenIzq = 20;
            int anchoInput = w - (margenIzq * 2);

            // Alto fijo para inputs — no depende de h
            int altoInput = 32;
            int altoLabel = 25;
            int espacio = 10; // espacio entre campos

            // TÍTULO
            llbDetalles.Location = new Point(margenIzq, 15);
            llbDetalles.Size = new Size(anchoInput, 40);

            // ID
            int yID = 65;
            lblID.Location = new Point(margenIzq, yID);
            lblID.Size = new Size(anchoInput, altoLabel);
            textID.Location = new Point(margenIzq, yID + altoLabel + 2);
            textID.Size = new Size(anchoInput, altoInput);

            // NOMBRE
            int yNombre = yID + altoLabel + altoInput + espacio + 15;
            lblNombre.Location = new Point(margenIzq, yNombre);
            lblNombre.Size = new Size(anchoInput, altoLabel);
            textNombre.Location = new Point(margenIzq, yNombre + altoLabel + 2);
            textNombre.Size = new Size(anchoInput, altoInput);

            // STOCK y PRECIO — misma fila
            int yStockPrecio = yNombre + altoLabel + altoInput + espacio + 15;
            int anchoCampo = (int)((anchoInput - 10) / 2);

            lblStock.Location = new Point(margenIzq, yStockPrecio);
            lblStock.Size = new Size(anchoCampo, altoLabel);
            textStock.Location = new Point(margenIzq, yStockPrecio + altoLabel + 2);
            textStock.Size = new Size(anchoCampo, altoInput);

            lblPrecio.Location = new Point(margenIzq + anchoCampo + 10, yStockPrecio);
            lblPrecio.Size = new Size(anchoCampo, altoLabel);
            textPV.Location = new Point(margenIzq + anchoCampo + 10, yStockPrecio + altoLabel + 2);
            textPV.Size = new Size(anchoCampo, altoInput);

            // CATEGORÍA
            int yCategoria = yStockPrecio + altoLabel + altoInput + espacio + 15;
            lblCategoria.Location = new Point(margenIzq, yCategoria);
            lblCategoria.Size = new Size(anchoInput, altoLabel);
            BoxPrueba.Location = new Point(margenIzq, yCategoria + altoLabel + 2);
            BoxPrueba.Size = new Size(anchoInput, altoInput);

            // BOTONES — siempre pegados al fondo
            int yBotones = h - 70;
            int anchoBtn = (int)((anchoInput - 20) / 3);
            btnNuevo.Location = new Point(margenIzq, yBotones);
            btnNuevo.Size = new Size(anchoBtn, 50);
            btnGuardar.Location = new Point(margenIzq + anchoBtn + 10, yBotones);
            btnGuardar.Size = new Size(anchoBtn, 50);
            btnEliminar.Location = new Point(margenIzq + (anchoBtn + 10) * 2, yBotones);
            btnEliminar.Size = new Size(anchoBtn, 50);

            pnlDetalles.Invalidate();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FormInventario_Load(object sender, EventArgs e)
        {
            pnlDetalles_Resize(this, EventArgs.Empty);
            CargarInventario();                   // LLAMAMOS AL MÉTODO PARA CARGAR LOS DATOS DE PRUEBA EN LA TABLA, LUEGO LO ELIMINAMOS O LO MODIFICAMOS PARA QUE CARGUE LOS DATOS REALES DESDE LA BASE DE DATOS
            pnlBD_Resize(this, EventArgs.Empty); // 
        }
        private void CargarInventario() // CON ESTE MÉTODO LLENAMOS LA TABLA CON DATOS DE PRUEBA PARA VER CÓMO QUEDA, LUEGO LO ELIMINAMOS O LO MODIFICAMOS PARA QUE CARGUE LOS DATOS REALES DESDE LA BASE DE DATOS
        {
            var lista = _ServicioInventario.Ver_Productos(out String errorMessage);//OBTENEMOS LA LISTA DE PRODUCTOS DESDE EL SERVICIO DE INVENTARIO, SI OCURRE UN ERROR SE ASIGNA UN MENSAJE A errorMessage

            if (!string.IsNullOrEmpty(errorMessage))
            {
                LabelError.Text = errorMessage;
                LabelError.Visible = true;
                return;
            }

            dtgBDInv.Rows.Clear(); // Limpio la tabla antes de cargar los datos

            foreach (var producto in lista)
            {
                dtgBDInv.Rows.Add( //añade una nueva fila a la tabla con los datos del producto
                    producto.IdProducto,
                    producto.Nombre,
                    producto.IdCategoria,
                    producto.Cantidad,
                    producto.Precio,
                    producto.Estado ? "Activo" : "Inactivo");
            }

        }

        private void pnlBD_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbBuscarBD_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlBuscarBD_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void btnGuardarBD_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnGuardarBD, e, 15);
        }


        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textID_Leave(object sender, EventArgs e)
        {
            if (textID.Text == "")
            {
                textID.Text = "Ej: 4011";
                textID.ForeColor = Color.DarkGray;
            }
        }

        private void textID_Enter(object sender, EventArgs e)
        {
            if (textID.Text == "Ej: 4011")
            {
                textID.Text = "";
                textID.ForeColor = Color.Black;
            }
        }

        private void textNombre_Leave(object sender, EventArgs e)
        {
            if (textNombre.Text == "")
            {
                textNombre.Text = "Nombre producto...";
                textNombre.ForeColor = Color.DarkGray;
            }
        }

        private void textNombre_Enter(object sender, EventArgs e)
        {
            if (textNombre.Text == "Nombre producto...")
            {
                textNombre.Text = "";
                textNombre.ForeColor = Color.Black;
            }
        }

        private void textStock_Leave(object sender, EventArgs e)
        {
            if (textStock.Text == "")
            {
                textStock.Text = "0";
                textStock.ForeColor = Color.DarkGray;
            }
        }

        private void textStock_Enter(object sender, EventArgs e)
        {
            if (textStock.Text == "0")
            {
                textStock.Text = "";
                textStock.ForeColor = Color.Black;
            }
        }

        private void textPV_Leave(object sender, EventArgs e)
        {
            if (textPV.Text == "")
            {
                textPV.Text = "0";
                textPV.ForeColor = Color.DarkGray;
            }
        }

        private void textPV_Enter(object sender, EventArgs e)
        {
            if (textPV.Text == "0")
            {
                textPV.Text = "";
                textPV.ForeColor = Color.Black;
            }
        }

        private void BuscarBD_Click(object sender, EventArgs e)
        {

        }

        private void dtgBDInv_DoubleClick(object sender, EventArgs e)
        {
            if (dtgBDInv.CurrentRow == null) return; // Si no hay una fila seleccionada, salimos del método
            DataGridViewRow fila = dtgBDInv.CurrentRow; // Obtenemos la fila seleccionada

            textID.Text = fila.Cells[0].Value.ToString();
            textNombre.Text = fila.Cells[1].Value.ToString();
            BoxPrueba.Text = fila.Cells[2].Value.ToString();
            textStock.Text = fila.Cells[3].Value.ToString();
            textPV.Text = fila.Cells[4].Value.ToString();

            //Bloquearemos el campo de ID y nombre
            textID.ReadOnly = true;
            textID.Enabled = false;
            textNombre.ReadOnly = true;
            textNombre.Enabled = false;

            _modoEdicion = true; //activamos el modo edición para que al guardar se actualice el producto en lugar de crear uno nuevo
        }
    }
}
