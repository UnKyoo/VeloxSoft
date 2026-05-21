using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using VeloxSoft.Models;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormClientes : Form
    {
        private Panel? pnlMiniFormulario;
        private Panel? pnlBarraMiniFormulario;
        private Button? btnCerrarMiniFormulario;

        private bool moviendoMiniFormulario = false;
        private Point puntoInicioMouse;
        private Point puntoInicioPanel;
        private readonly ServicioClientes _ServicioClientes;
        public FormClientes(ServicioClientes servicioClientes)
        {
            _ServicioClientes = servicioClientes;
            InitializeComponent();
            CrearMiniFormulario();
            btnAgregar.Click += btnAgregar_Click;
            this.DoubleBuffered = true;

            pnlFormulario_Resize(this, EventArgs.Empty);
            InicializarFormulariosBD();
            InicializarFiltros();
            // Evita el efecto de "congelado" o parpadeo
            this.DoubleBuffered = true;
          
        }

        // -------------------------- lógica GIL ---------------------------------
        private void FormClientes_Load(object sender, EventArgs e) //cuando se carga el formulario, se llama a este método para cargar los clientes en el DataGridView
        {
            var lista = _ServicioClientes.Ver_Clientes(out string errorMessage);
            _formTablaClientes?.CargarClientes(lista);

            var colonias = _ServicioClientes.Ver_Colonias(out string err2);
            _formTablaColonias?.CargarColonias(colonias);

            var direcciones = _ServicioClientes.Ver_Direcciones(out string err3);
            _formTablaDireccion?.CargarDirecciones(direcciones);

            if (!string.IsNullOrEmpty(errorMessage)) //si hay un mensaje de error, se muestra en un MessageBox y no se carga nada en el DataGridViewf
            {
                MessageBox.Show(errorMessage);
                return;
            }

            CargarDirecciones();

            cbFilColonia.Items.Clear();
            cbFilColonia.Items.Add(new Colonia { Id = 0, Texto = "Todas" });
            foreach (var col in colonias)
                cbFilColonia.Items.Add(col);
            cbFilColonia.SelectedIndex = 0;
            cbFilColonia.SelectedIndexChanged += cbColonia_SelectedIndexChanged_1;
        }

        private void ImprimirError(string mensaje)
        {
            LabelError.Text = mensaje;
            LabelError.ForeColor = Color.Red;
            LabelError.Visible = true;
        }   

        private void LimpiarMiniFormulario()
        {
            if (pnlMiniFormulario == null) return;

            // Limpiar campos de dirección
            (pnlMiniFormulario.Controls["txtNumeroCasa"] as TextBox)!.Text = "";
            (pnlMiniFormulario.Controls["txtCalle"] as TextBox)!.Text = "";
            (pnlMiniFormulario.Controls["txtCruzamiento"] as TextBox)!.Text = "";
            (pnlMiniFormulario.Controls["txtReferencia"] as TextBox)!.Text = "";
            (pnlMiniFormulario.Controls["cbColonia"] as ComboBox)!.SelectedIndex = -1;

            // Limpiar labels de mensaje
            (pnlMiniFormulario.Controls["lblMsgColonia"] as Label)!.Text = "";
            (pnlMiniFormulario.Controls["lblMsgDireccion"] as Label)!.Text = "";

            // Ocultar panel de nueva colonia si estaba abierto
            (pnlMiniFormulario.Controls["pnlNuevaColonia"] as Panel)!.Visible = false;
        }

        private void cbColonia_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            BusquedaCliente();

        }

        //Sanitizado - Validación de los campos de texto para evitar caracteres no deseados y limitar la longitud
        private void textNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return;

            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;

            if (textNumero.Text.Length >= 10)
                e.Handled = true;
        }

        private void textNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return;

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;

            if (textNombre.Text.Length >= 50)
                e.Handled = true;
        }

        private void textApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return;

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;

            if (textApellido.Text.Length >= 50)
                e.Handled = true;
        }

        private void textApodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return;

            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;

            if (textApodo.Text.Length >= 30)
                e.Handled = true;
        }

        //fin de sanitización y validacion.

        //método para cargar direcciones en el combobox

        private void CargarDirecciones()
        {
            var lista = _ServicioClientes.Ver_Direcciones(out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            textDIreccion.Items.Clear();
            foreach (var dir in lista)
                textDIreccion.Items.Add(dir); // ToString() genera el texto del ComboBox

            textDIreccion.SelectedIndex = -1;
        }

        //Método para cargar colonias en el combobox del mini formulario

        private void CargarColonias(ComboBox cb)
        {
            var lista = _ServicioClientes.Ver_Colonias(out string errorMessage);
            if (!string.IsNullOrEmpty(errorMessage)) { MessageBox.Show(errorMessage); return; }

            cb.Items.Clear();
            foreach (var colonia in lista)
                cb.Items.Add(colonia);

            cb.SelectedIndex = -1;
        }

        //Metodo para llamar al filtro dentro del cbFILColonia y en el botonBuscar
        private void BusquedaCliente()
        {
            string? id = string.IsNullOrWhiteSpace(textBuscarC.Text) 
            ? null : textBuscarC.Text.Trim();
            //is ColoniaItem verifia si el objeto seleccionado es realmente del modelo ColoniaItem, si es verdadero lo convierte al tipo del dato del modelom y lo guarda en la variable col
            string? colonia = cbFilColonia.SelectedItem is Colonia col && col.Id != 0 // && col.id!=0 verifica que no sea la opción "Todas" que tiene id 0, si es diferente de 0 entonces se asigna el texto de la colonia al filtro, de lo contrario se asigna null para no filtrar por colonia
            ? col.Texto : null;

            string tablaActual = cbTablas.SelectedItem?.ToString() ?? "Clientes"; //Por si no escoge ninguna tabla , que por defecto sea Clientes

            // 2. Evaluar el flujo según la tabla seleccionada
            if (tablaActual == "Clientes")
            {
                // Usamos el flujo de Clientes nativo (retorna List<Cliente>)
                var listaClientes = _ServicioClientes.Ver_Clientes(out string errorMessage, id, colonia);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    
                    return;
                }

                MostrarFormEnPanel(_formTablaClientes);
                _formTablaClientes?.CargarClientes(listaClientes);
            }
            else if (tablaActual == "Dirección")
            {
                // ¡Aquí está el cambio! Usamos el método filtrado que acabamos de ajustar (retorna List<Direccion>)
                var listaDirecciones = _ServicioClientes.Ver_Direcciones(out string errorMessage, id, colonia);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    return;
                }

                MostrarFormEnPanel(_formTablaDireccion);
                _formTablaDireccion?.CargarDirecciones(listaDirecciones);
            }
        }

     

        //Boton de guardar para añadir y/o actualizar clientes
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrWhiteSpace(textNumero.Text)  ||
                string.IsNullOrWhiteSpace(textNombre.Text) ||
                string.IsNullOrWhiteSpace(textApellido.Text))
            {
                LabelError.Text = "Teléfono, nombre y apellido son obligatorios.";
                LabelError.ForeColor = Color.Red;
                LabelError.Visible = true;
                return;
            }

            // 2. Validar que el teléfono tenga exactamente 10 dígitos
            if (textNumero.Text.Length != 10)
            {
                LabelError.Text = "El número telefónico debe tener 10 dígitos.";
                LabelError.ForeColor = Color.Red;
                LabelError.Visible = true;
                return;
            }

            // 3. Validar que haya dirección seleccionada
            if (textDIreccion.SelectedItem == null)
            {
                LabelError.Text = "Selecciona una dirección.";
                LabelError.ForeColor = Color.Red;
                LabelError.Visible = true;
                return;
            }

            // 4. Obtener valores
            long idCel = long.Parse(textNumero.Text.Trim());
            string nombre = textNombre.Text.Trim();
            string apellido = textApellido.Text.Trim();
            string apodo = textApodo.Text == "Apodo..." ? "" : textApodo.Text.Trim();
            Direccion direccion = (Direccion)textDIreccion.SelectedItem;

            // 5. Llamar al servicio
            string mensaje = _ServicioClientes.Insertar_Cliente(idCel, nombre, apellido, apodo, direccion.Id, out string errorMessage);

            // 6. Procesar respuesta
            if (!string.IsNullOrEmpty(errorMessage))
            {
                LabelError.Text = errorMessage;
                LabelError.ForeColor = Color.Red;
                LabelError.Visible = true;
                return;
            }

            LabelError.Text = mensaje;
            LabelError.ForeColor = Color.Green;
            LabelError.Visible = true;

            // 7. Recargar tabla y limpiar campos
            var lista = _ServicioClientes.Ver_Clientes(out string err);
            _formTablaClientes?.CargarClientes(lista);

            var colonias = _ServicioClientes.Ver_Colonias(out string err2);
            _formTablaColonias?.CargarColonias(colonias);

            var direcciones = _ServicioClientes.Ver_Direcciones(out string err3);
            _formTablaDireccion?.CargarDirecciones(direcciones);

            LimpiarCampos();
        }
        //Limpiar campos del formulario después de guardar un cliente, para que el usuario pueda ingresar otro cliente sin tener que borrar los campos manualmente. También se limpia el mensaje de error para evitar confusiones.
        private void LimpiarCampos()
        {
            textNumero.Text = "";
            textNumero.ForeColor = Color.DarkGray;

            textNombre.Text = "";
            textNombre.ForeColor = Color.DarkGray;

            textApellido.Text = "";
            textApellido.ForeColor = Color.DarkGray;

            textApodo.Text = "";
            textApodo.ForeColor = Color.DarkGray;

            textDIreccion.SelectedIndex = -1;

            LabelError.Text = "";
            LabelError.Visible = false;
        }

        private void textDIreccion_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* if (textDIreccion.SelectedItem is Direccion seleccionada)
            {
                MessageBox.Show($"ID: {seleccionada.Id}\n{seleccionada}");
            }*/
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscarC_Click(object? sender, EventArgs e)
        {
            BusquedaCliente();
        }

        //---------------------- DISEÑO ARMANDO ---------------------------------

        //Diseño de estetica y botonoes

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

            int nuevaX = puntoInicioPanel.X + delta.X;
            int nuevaY = puntoInicioPanel.Y + delta.Y;

            int minX = pnlClientes.Left;
            int minY = pnlClientes.Top;

            int maxX = pnlClientes.Right - pnlMiniFormulario.Width;
            int maxY = pnlClientes.Bottom - pnlMiniFormulario.Height;

            if (nuevaX < minX) nuevaX = minX;
            if (nuevaY < minY) nuevaY = minY;

            if (nuevaX > maxX) nuevaX = maxX;
            if (nuevaY > maxY) nuevaY = maxY;

            pnlMiniFormulario.Location = new Point(nuevaX, nuevaY);
        }

        private void pnlMiniFormulario_MouseUp(object? sender, MouseEventArgs e)
        {
            moviendoMiniFormulario = false;
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

        // Variables
        private FormTablaClientes? _formTablaClientes;
        private FormTablaColonias? _formTablaColonias;
        private FormTablaDireccion? _formTablaDireccion;
        private Form? _formActual;

        private void MostrarFormEnPanel(Form form)
        {
            if (_formActual != null)
                _formActual.Hide();

            if (!pnlBD.Controls.Contains(form))
                pnlBD.Controls.Add(form);

            form.Show();
            _formActual = form;
        }

        private void InicializarFormulariosBD()
        {
            _formTablaClientes = new FormTablaClientes();
            _formTablaColonias = new FormTablaColonias();
            _formTablaDireccion = new FormTablaDireccion();

            pnlBD.Controls.Add(_formTablaClientes);
            pnlBD.Controls.Add(_formTablaColonias);
            pnlBD.Controls.Add(_formTablaDireccion);

            _formTablaColonias.Hide();
            _formTablaDireccion.Hide();
            _formTablaClientes.Show();
            _formActual = _formTablaClientes;
        }

        private void InicializarFiltros()
        {
            // cbTablas — selecciona qué tabla ver
            cbTablas.Items.Clear();
            cbTablas.Items.Add("Clientes");
            cbTablas.Items.Add("Colonias");
            cbTablas.Items.Add("Dirección");
            cbTablas.SelectedIndex = 0;
            cbTablas.SelectedIndexChanged += cbTablas_SelectedIndexChanged;

            // cbColonia — se llena cuando se carguen datos de BD
            cbFilColonia.Items.Clear();
            cbFilColonia.Items.Add("Todas");
            cbFilColonia.SelectedIndex = 0;


            // btnBuscarC — busca por número de teléfono
            btnBuscarC.Click += btnBuscarC_Click;
        }

        // ── EVENTOS ────────────────────────────────────────
        private void cbTablas_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (cbTablas.SelectedItem?.ToString())
            {
                case "Clientes":
                    if (_formTablaClientes != null)
                        MostrarFormEnPanel(_formTablaClientes);
                        cbFilColonia.SelectedIndex = 0;
                        cbFilColonia.Visible = true;
                    break;
                case "Colonias":
                    if (_formTablaColonias != null)
                        MostrarFormEnPanel(_formTablaColonias);
                        cbFilColonia.SelectedIndex = 0;
                        cbFilColonia.Visible = false;
                    break;
                case "Dirección":
                    if (_formTablaDireccion != null)
                        MostrarFormEnPanel(_formTablaDireccion);
                        cbFilColonia.SelectedIndex = 0;
                        cbFilColonia.Visible = true;
                    break;
            }
        }


        //////////// FIN DE LAS TABLAS ////////////////

        // ── RESIZE DEL PANEL BOTONES ───────────────────────
        private void pnlBotones_Resize(object sender, EventArgs e)
        {
            int w = pnlBotones.Width;
            int h = pnlBotones.Height;
            int margenH = 15;
            int altoControl = 28;
            int espacioPequeño = 5;
            int espacioGrande = 20;

            Label lblTablas = ObtenerLabel("lblTablas", "Tabla:");
            Label lblColonia = ObtenerLabel("lblColonia", "Colonia:");

            bool dosLineas = w < 850;

            if (!dosLineas)
            {
                int y1 = (h / 2) - altoControl - 4;
                int y2 = (h / 2) - 6;

                lblTablas.Location = new Point(margenH, y2);        // sin el +4
                lblTablas.Size = new Size(lblTablas.PreferredWidth, 24);

                lblColonia.Location = new Point(cbTablas.Right + espacioGrande, y2);  // sin el +4
                lblColonia.Size = new Size(lblColonia.PreferredWidth, 24);

                // BUSCAR
                lblBuscarC.Location = new Point(margenH, y1 + 4);
                textBuscarC.Location = new Point(lblBuscarC.Right + espacioPequeño, y1);
                textBuscarC.Size = new Size(w - lblBuscarC.Right - espacioPequeño - 35 - 2 - margenH, altoControl);
                btnBuscarC.Location = new Point(textBuscarC.Right + 2, y1);
                btnBuscarC.Size = new Size(35, altoControl);

                // TABLAS
                int anchoTabla = (int)((w - margenH * 2) * 0.30);
                lblTablas.Location = new Point(margenH, y2 + 4);
                lblTablas.Size = new Size(lblTablas.PreferredWidth, altoControl);
                cbTablas.Location = new Point(lblTablas.Right + espacioPequeño, y2);
                cbTablas.Size = new Size(anchoTabla, altoControl);

                // COLONIA
                lblColonia.Location = new Point(cbTablas.Right + espacioGrande, y2 + 4);
                lblColonia.Size = new Size(lblColonia.PreferredWidth, altoControl);
                cbFilColonia.Location = new Point(lblColonia.Right + espacioPequeño, y2);
                cbFilColonia.Size = new Size(w - lblColonia.Right - espacioPequeño - margenH, altoControl);
            }
            else
            {
                // ── DOS LÍNEAS ──
                int margenV = 10;
                int anchoDisponible = w - (margenH * 2);
                int anchoBtnBuscar = 35;

                // FILA 1: BUSCAR
                int y1 = margenV;
                lblBuscarC.Location = new Point(margenH, y1 + 4);
                int anchoTxtBuscar = anchoDisponible - lblBuscarC.Width - anchoBtnBuscar - 10;
                textBuscarC.Location = new Point(lblBuscarC.Right + espacioPequeño, y1);
                textBuscarC.Size = new Size(anchoTxtBuscar, altoControl);
                btnBuscarC.Location = new Point(textBuscarC.Right + 2, y1);
                btnBuscarC.Size = new Size(anchoBtnBuscar, altoControl);

                // FILA 2: COMBOS
                int y2 = y1 + altoControl + 10;
                int anchoGrupo = (anchoDisponible - espacioGrande) / 2;

                // TABLAS
                lblTablas.Location = new Point(margenH, y2 + 4);
                lblTablas.Size = new Size(lblTablas.PreferredWidth, altoControl);
                cbTablas.Location = new Point(lblTablas.Right + espacioPequeño, y2);
                cbTablas.Size = new Size(anchoGrupo - lblTablas.Width - espacioPequeño, altoControl);

                // COLONIA
                lblColonia.Location = new Point(cbTablas.Right + espacioGrande, y2 + 4);
                lblColonia.Size = new Size(lblColonia.PreferredWidth, altoControl);
                cbFilColonia.Location = new Point(lblColonia.Right + espacioPequeño, y2);
                cbFilColonia.Size = new Size(w - (lblColonia.Right + espacioPequeño) - margenH, altoControl);
            }

            pnlBotones.Invalidate();
        }


        // Helper para crear labels dinámicos una sola vez
        private Label ObtenerLabel(string nombre, string texto)
        {
            foreach (Control c in pnlBotones.Controls)
                if (c.Name == nombre) return (Label)c;

            Label lbl = new Label();
            lbl.Name = nombre;
            lbl.Text = texto;
            lbl.AutoSize = false;
            lbl.Font = new Font("Century Gothic", 12F);
            lbl.ForeColor = Color.FromArgb(59, 109, 17);
            pnlBotones.Controls.Add(lbl);
            return lbl;
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
        /////////////////Fin diseño/////////////////

        //Resize de los paneles para ajustar el tamaño de los internos
        private void pnlClientes_Resize(object sender, EventArgs e)
        {
            int w = pnlClientes.Width;
            int h = pnlClientes.Height;
            int margen = 12;
            int espacioH = 10;

            int anchoIzq = (int)(w * 0.40) - margen;
            int anchoDer = w - anchoIzq - (margen * 3) - espacioH;

            // Alto del pnlBotones — más alto en pantalla pequeña para 2 filas
            int altoBotones = w < 900 ? 110 : 75;

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

            pnlClientes.Invalidate();

        }
        //Fin de los resize de los paneles para ajustar el tamaño de los internos
        //Rezise del Formulario para ajustar el tamaño de los elementos internos
        private void pnlFormulario_Resize(object sender, EventArgs e)
        {

            int w = pnlFormulario.Width;
            int h = pnlFormulario.Height;
            int margen = 40;
            int anchoInput = w - (margen * 2);
            int altoInput = 32;
            int altoLabel = 23;
            int espacioEntreGrupos = 18;

            // TÍTULO
            lblTituloForm.Location = new Point(margen - 10, 15);

            // NÚMERO TELEFÓNICO
            int yNumero = 80;
            lblNumero.Location = new Point(margen, yNumero);
            textNumero.Location = new Point(margen, yNumero + altoLabel + 2);
            textNumero.Size = new Size(anchoInput, altoInput);

            // NOMBRE
            int yNombre = textNumero.Bottom + espacioEntreGrupos;
            lblNombre.Location = new Point(margen, yNombre);
            textNombre.Location = new Point(margen, yNombre + altoLabel + 2);
            textNombre.Size = new Size(anchoInput, altoInput);

            // APELLIDO
            int yApellido = textNombre.Bottom + espacioEntreGrupos;
            lblApellido.Location = new Point(margen, yApellido);
            textApellido.Location = new Point(margen, yApellido + altoLabel + 2);
            textApellido.Size = new Size(anchoInput, altoInput);

            // APODO
            int yApodo = textApellido.Bottom + espacioEntreGrupos;
            lblApodo.Location = new Point(margen, yApodo);
            textApodo.Location = new Point(margen, yApodo + altoLabel + 2);
            textApodo.Size = new Size(anchoInput, altoInput);

            // DIRECCIÓN
            int yDireccion = textApodo.Bottom + espacioEntreGrupos;
            lblDireccion.Location = new Point(margen, yDireccion);

            int anchoBuscar = 42;
            int espacioBuscar = 8;

            textDIreccion.Location = new Point(margen, yDireccion + altoLabel + 2);
            textDIreccion.Size = new Size(anchoInput - anchoBuscar - espacioBuscar, altoInput);

            btnAgregar.Location = new Point(textDIreccion.Right + espacioBuscar, textDIreccion.Top);
            btnAgregar.Size = new Size(anchoBuscar, altoInput);

            // BOTONES — pegados al fondo
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



            pnlFormulario.Invalidate();
        }
        //FIn del resize del Formulario para ajustar el tamaño de los elementos internos

        //Redibujar el panel formulario con bordes redondeados cada vez que se pinta
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

        //Fin de redibujar el panel formulario con bordes redondeados cada vez que se pinta


        //Redibujar los botones con bordes redondeados cada vez que se pinta

        private void btnGuardar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnGuardar, e, 15);
        }

        private void btnEliminar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnEliminar, e, 15);
        }


        private void btnLimpiar_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnLimpiar, e, 15);
        }

        private void btnBuscarF_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnAgregar, e, 5);
        }

        //Fin de redibujar los botones con bordes redondeados cada vez que se pinta

        //Diseño de los textbox para que tengan un texto de ejemplo y se borre al hacer click, y vuelva a aparecer si no se escribe nada

        //Numero


        private void btnAgregar_Click(object? sender, EventArgs e)
        {
            if (pnlMiniFormulario != null)
            {
                ComboBox? cb = pnlMiniFormulario.Controls["cbColonia"] as ComboBox;
                if (cb != null) CargarColonias(cb);

                pnlMiniFormulario.Visible = true;
                pnlMiniFormulario.BringToFront();
            }
        }

        private void btnCerrarMiniFormulario_Click(object? sender, EventArgs e)
        {
            if (pnlMiniFormulario != null)
            {
                pnlMiniFormulario.Visible = false;
            }
        }
        //Fin de diseño de los textbox para que tengan un texto de ejemplo y se borre al hacer click, y vuelva a aparecer si no se escribe nada
        private void CrearMiniFormulario()
        {
            pnlMiniFormulario = new Panel();
            btnCerrarMiniFormulario = new Button();

            pnlMiniFormulario.Name = "pnlMiniFormulario";
            pnlMiniFormulario.Size = new Size(420, 430);
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
            lblTituloMini.Text = "Agregar dirección";
            lblTituloMini.Font = new Font("Century Gothic", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloMini.ForeColor = Color.FromArgb(59, 109, 17);
            lblTituloMini.Location = new Point(12, 8);
            lblTituloMini.MouseDown += pnlMiniFormulario_MouseDown;
            lblTituloMini.MouseMove += pnlMiniFormulario_MouseMove;
            lblTituloMini.MouseUp += pnlMiniFormulario_MouseUp;
            pnlBarraMiniFormulario.Controls.Add(lblTituloMini);
            pnlMiniFormulario.Controls.Add(pnlBarraMiniFormulario);

            Font fuente = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Color colorLabel = Color.FromArgb(59, 109, 17);
            Color colorInput = Color.FromArgb(250, 254, 247);

            // ── Número de casa ──
            Label lblNumeroCasa = new Label();
            lblNumeroCasa.AutoSize = true;
            lblNumeroCasa.Font = fuente;
            lblNumeroCasa.ForeColor = colorLabel;
            lblNumeroCasa.Location = new Point(20, 55);
            lblNumeroCasa.Name = "lblNumeroCasa";
            lblNumeroCasa.Text = "Número de casa";

            TextBox txtNumeroCasa = new TextBox();
            txtNumeroCasa.BackColor = colorInput;
            txtNumeroCasa.Font = fuente;
            txtNumeroCasa.Location = new Point(20, 80);
            txtNumeroCasa.Name = "txtNumeroCasa";
            txtNumeroCasa.Size = new Size(150, 32);

            // ── Calle ──
            Label lblCalle = new Label();
            lblCalle.AutoSize = true;
            lblCalle.Font = fuente;
            lblCalle.ForeColor = colorLabel;
            lblCalle.Location = new Point(200, 55);
            lblCalle.Name = "lblCalle";
            lblCalle.Text = "Calle";

            TextBox txtCalle = new TextBox();
            txtCalle.BackColor = colorInput;
            txtCalle.Font = fuente;
            txtCalle.Location = new Point(200, 80);
            txtCalle.Name = "txtCalle";
            txtCalle.Size = new Size(180, 32);

            // ── Cruzamiento ──
            Label lblCruzamiento = new Label();
            lblCruzamiento.AutoSize = true;
            lblCruzamiento.Font = fuente;
            lblCruzamiento.ForeColor = colorLabel;
            lblCruzamiento.Location = new Point(20, 125);
            lblCruzamiento.Name = "lblCruzamiento";
            lblCruzamiento.Text = "Cruzamiento";

            TextBox txtCruzamiento = new TextBox();
            txtCruzamiento.BackColor = colorInput;
            txtCruzamiento.Font = fuente;
            txtCruzamiento.Location = new Point(20, 150);
            txtCruzamiento.Name = "txtCruzamiento";
            txtCruzamiento.Size = new Size(170, 32);

            // ── Referencia ──
            Label lblReferencia = new Label();
            lblReferencia.AutoSize = true;
            lblReferencia.Font = fuente;
            lblReferencia.ForeColor = colorLabel;
            lblReferencia.Location = new Point(210, 125);
            lblReferencia.Name = "lblReferencia";
            lblReferencia.Text = "Referencia";

            TextBox txtReferencia = new TextBox();
            txtReferencia.BackColor = colorInput;
            txtReferencia.Font = fuente;
            txtReferencia.Location = new Point(210, 150);
            txtReferencia.Name = "txtReferencia";
            txtReferencia.Size = new Size(170, 32);

            // ── Colonia ──
            Label lblColonia = new Label();
            lblColonia.AutoSize = true;
            lblColonia.Font = fuente;
            lblColonia.ForeColor = colorLabel;
            lblColonia.Location = new Point(20, 195);
            lblColonia.Name = "lblColonia";
            lblColonia.Text = "Colonia";

            ComboBox cbColonia = new ComboBox();
            cbColonia.BackColor = colorInput;
            cbColonia.Font = fuente;
            cbColonia.Location = new Point(20, 220);
            cbColonia.Name = "cbColonia";
            cbColonia.Size = new Size(330, 31);
            cbColonia.DropDownStyle = ComboBoxStyle.DropDownList;

            Button btnAgregarColonia = new Button();
            btnAgregarColonia.BackColor = Color.FromArgb(59, 109, 17);
            btnAgregarColonia.FlatAppearance.BorderSize = 0;
            btnAgregarColonia.FlatStyle = FlatStyle.Flat;
            btnAgregarColonia.ForeColor = Color.White;
            btnAgregarColonia.Font = new Font("Century Gothic", 16F, FontStyle.Bold);
            btnAgregarColonia.Location = new Point(358, 218);
            btnAgregarColonia.Name = "btnAgregarColonia";
            btnAgregarColonia.Size = new Size(36, 34);
            btnAgregarColonia.Text = "+";
            btnAgregarColonia.UseVisualStyleBackColor = false;

            // ── Label mensaje colonia ──
            Label lblMsgColonia = new Label();
            lblMsgColonia.Name = "lblMsgColonia";
            lblMsgColonia.AutoSize = false;
            lblMsgColonia.Size = new Size(374, 20);
            lblMsgColonia.Location = new Point(20, 258);
            lblMsgColonia.Font = new Font("Century Gothic", 9F);
            lblMsgColonia.TextAlign = ContentAlignment.MiddleLeft;
            lblMsgColonia.Text = "";

            // ── Panel nueva colonia (oculto) ──
            Panel pnlNuevaColonia = new Panel();
            pnlNuevaColonia.Name = "pnlNuevaColonia";
            pnlNuevaColonia.BackColor = Color.FromArgb(234, 243, 222);
            pnlNuevaColonia.Location = new Point(20, 280);
            pnlNuevaColonia.Size = new Size(374, 44);
            pnlNuevaColonia.Visible = false;

            TextBox txtNuevaColonia = new TextBox();
            txtNuevaColonia.BackColor = colorInput;
            txtNuevaColonia.Font = fuente;
            txtNuevaColonia.Location = new Point(4, 6);
            txtNuevaColonia.Name = "txtNuevaColonia";
            txtNuevaColonia.Size = new Size(240, 32);

            Button btnGuardarColonia = new Button();
            btnGuardarColonia.BackColor = Color.FromArgb(59, 109, 17);
            btnGuardarColonia.FlatAppearance.BorderSize = 0;
            btnGuardarColonia.FlatStyle = FlatStyle.Flat;
            btnGuardarColonia.ForeColor = Color.White;
            btnGuardarColonia.Font = fuente;
            btnGuardarColonia.Location = new Point(252, 4);
            btnGuardarColonia.Size = new Size(118, 36);
            btnGuardarColonia.Text = "Guardar";
            btnGuardarColonia.UseVisualStyleBackColor = false;


            pnlNuevaColonia.Controls.Add(txtNuevaColonia);
            pnlNuevaColonia.Controls.Add(btnGuardarColonia);

            // ── Label mensaje dirección ──
            Label lblMsgDireccion = new Label();
            lblMsgDireccion.Name = "lblMsgDireccion";
            lblMsgDireccion.AutoSize = false;
            lblMsgDireccion.Size = new Size(374, 20);
            lblMsgDireccion.Location = new Point(20, 340);
            lblMsgDireccion.Font = new Font("Century Gothic", 9F);
            lblMsgDireccion.TextAlign = ContentAlignment.MiddleLeft;
            lblMsgDireccion.Text = "";

            // ── Botón guardar dirección ──
            Button btnGuardarMini = new Button();
            btnGuardarMini.BackColor = Color.FromArgb(59, 109, 17);
            btnGuardarMini.FlatAppearance.BorderSize = 0;
            btnGuardarMini.FlatStyle = FlatStyle.Flat;
            btnGuardarMini.ForeColor = Color.White;
            btnGuardarMini.Font = fuente;
            btnGuardarMini.Location = new Point((pnlMiniFormulario.Width - 160) / 2, 365);
            btnGuardarMini.Name = "btnGuardarMini";
            btnGuardarMini.Size = new Size(160, 40);
            btnGuardarMini.Text = "Guardar dirección";
            btnGuardarMini.UseVisualStyleBackColor = false;

            // ── Eventos ──
            btnAgregarColonia.Click += (s, e) =>
            {
                lblMsgColonia.Text = "";
                lblMsgColonia.Visible = false;
                pnlNuevaColonia.Visible = true;
                txtNuevaColonia.Text = "";
                txtNuevaColonia.Focus();
            };

            btnGuardarColonia.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNuevaColonia.Text))
                {
                    lblMsgColonia.Text = "Escribe el nombre de la colonia.";
                    lblMsgColonia.ForeColor = Color.Red;
                    return;
                }

                string mensaje = _ServicioClientes.Insertar_Colonia(
                    txtNuevaColonia.Text.Trim(), out string errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblMsgColonia.Text = errorMessage;
                    lblMsgColonia.ForeColor = Color.Red;
                    return;
                }

                string nuevaColonia = txtNuevaColonia.Text.Trim();
                CargarColonias(cbColonia);
                var colonias = _ServicioClientes.Ver_Colonias(out string err);
                _formTablaColonias?.CargarColonias(colonias);

                // Actualizar también el filtro cbFilColonia
                cbFilColonia.Items.Clear();
                cbFilColonia.Items.Add(new Colonia { Id = 0, Texto = "Todas" });
                foreach (var col in colonias)
                    cbFilColonia.Items.Add(col);

                //añadimos la nueva colonia al filtro cbFilColonia
                for (int i = 0; i < cbColonia.Items.Count; i++)
                {
                    if (cbColonia.Items[i].ToString() == nuevaColonia)
                    {
                        cbColonia.SelectedIndex = i;
                        break;
                    }
                }

                pnlNuevaColonia.Visible = false;
                txtNuevaColonia.Text = "";
                lblMsgColonia.Text = mensaje;
                lblMsgColonia.ForeColor = Color.Green;
            };

            btnGuardarMini.Click += (s, e) =>
            {
                // 1. Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNumeroCasa.Text) ||
                    string.IsNullOrWhiteSpace(txtCalle.Text) ||
                    string.IsNullOrWhiteSpace(txtCruzamiento.Text))
                {
                    lblMsgDireccion.Text = "Número de casa, calle y cruzamiento son obligatorios.";
                    lblMsgDireccion.ForeColor = Color.Red;
                    return;
                }

                // 2. Validar que haya colonia seleccionada
                if (cbColonia.SelectedItem == null)
                {
                    lblMsgDireccion.Text = "Selecciona una colonia.";
                    lblMsgDireccion.ForeColor = Color.Red;
                    return;
                }

                // 3. Obtener el ID de la colonia seleccionada
                Colonia coloniaSeleccionada = (Colonia)cbColonia.SelectedItem;

                // 4. Llamar al servicio
                string mensaje = _ServicioClientes.Insertar_Direccion(
                    txtNumeroCasa.Text.Trim(),
                    txtCalle.Text.Trim(),
                    txtCruzamiento.Text.Trim(),
                    txtReferencia.Text.Trim(),
                    coloniaSeleccionada.Id,
                    out string errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblMsgDireccion.Text = errorMessage;
                    lblMsgDireccion.ForeColor = Color.Red;
                    return;
                }

                // 5. Éxito — recargar el ComboBox de direcciones y cerrar el mini formulario
                lblMsgDireccion.Text = mensaje;
                lblMsgDireccion.ForeColor = Color.Green;
                CargarDirecciones();
                var direcciones = _ServicioClientes.Ver_Direcciones(out string err);
                _formTablaDireccion?.CargarDirecciones(direcciones);
                LimpiarMiniFormulario();
                pnlMiniFormulario.Visible = false;
            };

            // ── Agregar controles en orden correcto ──
            pnlMiniFormulario.Controls.Add(lblNumeroCasa);
            pnlMiniFormulario.Controls.Add(txtNumeroCasa);
            pnlMiniFormulario.Controls.Add(lblCalle);
            pnlMiniFormulario.Controls.Add(txtCalle);
            pnlMiniFormulario.Controls.Add(lblCruzamiento);
            pnlMiniFormulario.Controls.Add(txtCruzamiento);
            pnlMiniFormulario.Controls.Add(lblReferencia);
            pnlMiniFormulario.Controls.Add(txtReferencia);
            pnlMiniFormulario.Controls.Add(lblColonia);
            pnlMiniFormulario.Controls.Add(cbColonia);
            pnlMiniFormulario.Controls.Add(btnAgregarColonia);
            pnlMiniFormulario.Controls.Add(lblMsgColonia);
            pnlMiniFormulario.Controls.Add(pnlNuevaColonia); // El panel de la colonia va "atrás"
            pnlMiniFormulario.Controls.Add(lblMsgDireccion);
            pnlMiniFormulario.Controls.Add(btnGuardarMini);  // El botón principal va "al frente" de todo




            // ── Botón cerrar ──
            btnCerrarMiniFormulario.Text = "X";
            btnCerrarMiniFormulario.Size = new Size(30, 26);
            btnCerrarMiniFormulario.Location = new Point(pnlMiniFormulario.Width - 40, 5);
            btnCerrarMiniFormulario.FlatStyle = FlatStyle.Flat;
            btnCerrarMiniFormulario.FlatAppearance.BorderSize = 0;
            btnCerrarMiniFormulario.BackColor = Color.FromArgb(163, 45, 45);
            btnCerrarMiniFormulario.ForeColor = Color.White;
            btnCerrarMiniFormulario.Click += btnCerrarMiniFormulario_Click;
            pnlBarraMiniFormulario.Controls.Add(btnCerrarMiniFormulario);

            this.Controls.Add(pnlMiniFormulario);
            pnlMiniFormulario.BringToFront();
        }


    }
}
