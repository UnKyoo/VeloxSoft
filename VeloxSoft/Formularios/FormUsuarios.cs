using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using VeloxSoft.Models;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormUsuarios : Form
    {
        private readonly ServicioUsuarios _ServicioUsuarios;
        private bool _modoEdicion = false;
        private bool _MostrarContrasenia = false;

        public FormUsuarios(ServicioUsuarios servicioUsuarios)
        {
            _ServicioUsuarios = servicioUsuarios;
            InitializeComponent();
            pnlUsuarios_Resize(this, EventArgs.Empty); // ← AGREGA
            pnlFormulario_Resize(this, EventArgs.Empty);
            pnlBotones_Resize(this, EventArgs.Empty);
            pnlBD_Resize(this, EventArgs.Empty);
        }

        // Logica de funcionamiento, no mezclar codigo de diseño con codigo de logica
        private void CargarUsuarios()
        {
            List<Usuario> Usuarios = _ServicioUsuarios.Buscar_Usuarios(textBuscarU.Text, cbCROL.Text, cbCSESION.Text, cbCESTADO.Text, out string errorMessage);

            dgvUsuariosDB.Rows.Clear();

            foreach (var usuario in Usuarios)
            {
                string rolTexto = usuario.Rol switch
                {
                    "0" => "Gerente",
                    "1" => "Administrador",
                    "2" => "Cajero",
                    _ => "Desconocido"
                };
                dgvUsuariosDB.Rows.Add(
                    usuario.Id,
                    usuario.Nombre,
                    rolTexto,
                    usuario.Secion ? "Conectado" : "Desconectado",
                    usuario.Estado ? "Activo" : "Inactivo");
            }
        }

        public void MostrarError(string mensaje)
        {
            LabelError2.Text = mensaje;
            LabelError2.ForeColor = Color.Red;
            LabelError2.Visible = true;
        }

        public void MostrarExito(string mensaje)
        {
            LabelError2.Text = mensaje;
            LabelError2.ForeColor = Color.Green;
            LabelError2.Visible = true;
        }

        public void LimpiarMensaje()
        {
            LabelError2.Visible = false;
            LabelError2.Text = "";
        }
        private void dgvUsuariosDB_Click(object sender, EventArgs e)
        {
            if (dgvUsuariosDB.SelectedRows[0].Cells[4].Value.ToString() == "Inactivo")
            {
                btnEliminar.BackColor = Color.Green;
                btnEliminar.Text = "Activar";
            }
            else
            {
                btnEliminar.BackColor = Color.FromArgb(163, 45, 45);
                btnEliminar.Text = "Eliminar";
            }
        }

        private void dgvUsuariosDB_DoubleClick(object sender, EventArgs e)
        {
            Limpiar();
            textID.Text = dgvUsuariosDB.CurrentRow.Cells[0].Value.ToString();
            ID_actual.Text = dgvUsuariosDB.CurrentRow.Cells[0].Value.ToString();
            textNombre.Text = dgvUsuariosDB.CurrentRow.Cells[1].Value.ToString();
            textRol.Text = dgvUsuariosDB.CurrentRow.Cells[2].Value.ToString();

            string Secion = dgvUsuariosDB.CurrentRow.Cells[3].Value.ToString();
            string Estado = dgvUsuariosDB.CurrentRow.Cells[4].Value.ToString();

            textNombre.Enabled = false;
            textNombre.ReadOnly = true;
            textNombre.ForeColor = Color.DarkGray;


            int rolInt = dgvUsuariosDB.CurrentRow.Cells[2].Value.ToString() switch
            {
                "Gerente" => 0,
                "Administrador" => 1,
                "Cajero" => 2,
                _ => 3
            };


            if (rolInt <= int.Parse(Program.UsuarioLogueado.Rol) || Secion == "Conectado")
            {
                textID.Enabled = false;
                textID.ReadOnly = true;
                textID.ForeColor = Color.DarkGray;
                textRol.Enabled = false;
                textRol.ForeColor = Color.DarkGray;
                textContra.Enabled = false;
                textContra.ReadOnly = true;
                textContra.ForeColor = Color.DarkGray;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;

                MostrarError("No es posible editar a este usuario porque su rol es igual al tuyo o está conectado.");
            }
            else
            {
                textID.Enabled = true;
                textID.ReadOnly = false;
                textID.ForeColor = Color.Black;
                textRol.Enabled = true;
                textRol.ForeColor = Color.Black;
                textContra.Enabled = true;
                textContra.ReadOnly = false;
                textContra.ForeColor = Color.Black;
            }

            if (Estado == "Inactivo")
            {
                btnEliminar.BackColor = Color.Green;
                btnEliminar.Text = "Activar";
            }

            _modoEdicion = true;
        }

        private void Limpiar()
        {
            textID.Text = "";
            ID_actual.Text = "";
            textNombre.Text = "";
            textRol.Text = "";
            textContra.Text = "";

            textID.Enabled = true;
            textID.ReadOnly = false;
            textID.ForeColor = Color.Black;
            textNombre.Enabled = true;
            textNombre.ReadOnly = false;
            textNombre.ForeColor = Color.Black;
            textRol.Enabled = true;
            textRol.ForeColor = Color.Black;
            textContra.Enabled = true;
            textContra.ReadOnly = false;
            textContra.ForeColor = Color.Black;

            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            btnEliminar.BackColor = Color.FromArgb(163, 45, 45);
            btnEliminar.Text = "Eliminar";

            LimpiarMensaje();

            _modoEdicion = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Sanitización 
            string Errores = string.Empty;
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(textID.Text))
            {
                Errores += "El campo ID no puede estar vacío.\n";
            }
            if (10 > textID.Text.Length)
            {
                Errores += "El campo ID debe tener 10 digitos.\n";
            }
            if (string.IsNullOrEmpty(textNombre.Text))
            {
                Errores += "El campo Nombre no puede estar vacío.\n";
            }
            if (string.IsNullOrEmpty(textRol.Text))
            {
                Errores += "El campo Rol no puede estar vacío.\n";
            }
            if (!string.IsNullOrEmpty(textContra.Text))
            {
                if (!Regex.IsMatch(textContra.Text, @"[A-Z]"))
                {
                    Errores += "La contraseña debe contener al menos una letra mayúscula.\n";
                }
                if (!Regex.IsMatch(textContra.Text, @"[a-z]"))
                {
                    Errores += "La contraseña debe contener al menos una letra minúscula.\n";
                }
                if (!Regex.IsMatch(textContra.Text, @"\d"))
                {
                    Errores += "La contraseña debe contener al menos un número.\n";
                }
                if (textContra.Text.Length < 8)
                {
                    Errores += "La contraseña debe tener al menos 8 caracteres.\n";
                }
            }

            string rol = textRol.Text switch
            {
                "Administrador" => "1",
                "Cajero" => "2",
                _ => "3"
            };
            if (_modoEdicion)
            {
                // Actualizar Usuario
                if (string.IsNullOrEmpty(Errores))
                {
                    _ServicioUsuarios.Actualizar_Usuario(textID.Text, ID_actual.Text, textContra.Text, rol, true, out errorMessage);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        MostrarExito("Usuario actualizado correctamente.");
                        Limpiar();
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(textContra.Text))
                {
                    Errores += "El campo Contraseña no puede estar vacío.\n";
                }
                if (string.IsNullOrEmpty(Errores))
                {
                    _ServicioUsuarios.Insertar_Usuario(textID.Text, textNombre.Text, rol, textContra.Text, out errorMessage);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        MostrarExito("Usuario insertado correctamente.");
                        Limpiar();
                    }
                }
            }
            if (!string.IsNullOrEmpty(Errores) || !string.IsNullOrEmpty(errorMessage))
            {
                MostrarError("No se pudo guardar el usuario debido a los siguientes errores:\n" + Errores + errorMessage);
            }

            CargarUsuarios();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuariosDB.SelectedRows.Count == 0)
            {
                MostrarError("Por favor, seleccione un usuario para eliminar.");
                return;
            }


            string idUsuario = dgvUsuariosDB.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
            int rolInt = dgvUsuariosDB.SelectedRows[0].Cells[2].Value.ToString() switch
            {
                "Gerente" => 0,
                "Administrador" => 1,
                "Cajero" => 2,
                _ => 3
            };

            if (rolInt <= int.Parse(Program.UsuarioLogueado.Rol))
            {
                MostrarError("No es posible eliminar a este usuario porque su rol es igual al tuyo.");
                return;
            }

            if (dgvUsuariosDB.SelectedRows[0].Cells[3].Value.ToString() == "Conectado")
            {
                MostrarError("No es posible eliminar a este usuario porque está conectado.");
                return;
            }


            var Evitemos_Errores = MessageBox.Show(
                $"¿Está seguro de que desea {btnEliminar.Text.ToString()} al Usuario con ID: {idUsuario}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (Evitemos_Errores == DialogResult.No) return;


            string EstadoUsuario = dgvUsuariosDB.SelectedRows[0].Cells[4].Value?.ToString() ?? "";
            bool estadoBool = EstadoUsuario == "Inactivo";
            _ServicioUsuarios.Eliminar_Usuario(idUsuario, estadoBool, out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MostrarError(errorMessage);
            }
            else
            {
                CargarUsuarios();
                Limpiar();
                MostrarExito("Usuario " + (estadoBool ? "activado" : "eliminado") + " correctamente.");
            }
        }
        private void btnBuscarU_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void textID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        private void textBuscarU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Fin logica de funcionamiento


        //Diseño estetico de esteticos y botonoes


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


        /////////////////Fin diseño/////////////////



        private void dgvUsuariosDB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlBotones_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void pnlBD_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void pnlFormulario_Paint(object sender, PaintEventArgs e)
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

        private void pnlFormulario_Resize(object sender, EventArgs e)
        {
            int w = pnlFormulario.Width;
            int h = pnlFormulario.Height;
            int margenGral = 40;
            int anchoInput = w - (margenGral * 2);

            int altoInput = 32;
            int altoLabel = 23;
            int espacioEntreGrupos = 18;

            // --- TÍTULO, ID, NOMBRE, ROL/ESTADO y CONTRASEÑA ---
            // (Mantenemos la lógica anterior para la parte superior)
            lblTituloForm.Location = new Point(margenGral - 10, 25);

            int yID = 100;
            lblID.Location = new Point(margenGral, yID);
            textID.Location = new Point(margenGral, yID + altoLabel + 2);
            textID.Size = new Size(anchoInput, altoInput);

            int yNombre = textID.Bottom + espacioEntreGrupos;
            lblNombre.Location = new Point(margenGral, yNombre);
            textNombre.Location = new Point(margenGral, yNombre + altoLabel + 2);
            textNombre.Size = new Size(anchoInput, altoInput);

            int yFilaDoble = textNombre.Bottom + espacioEntreGrupos;

            lblRol.Location = new Point(margenGral, yFilaDoble);

            textRol.Location = new Point(
                margenGral,
                yFilaDoble + altoLabel + 2
            );

            textRol.Size = new Size(anchoInput, altoInput);
            int yContra = textRol.Bottom + espacioEntreGrupos;
            lblContraseña.Location = new Point(margenGral, yContra);
            textContra.Location = new Point(margenGral, yContra + altoLabel + 2);
            textContra.Size = new Size(anchoInput, altoInput);

            // CONTRASEÑA — input más corto para dar espacio al botón
            int anchoBtnPass = 42;
            int espacioBtnPass = 6;

            textContra.Location = new Point(margenGral, yContra + altoLabel + 2);
            textContra.Size = new Size(anchoInput - anchoBtnPass - espacioBtnPass, altoInput);

            // Botón ver/ocultar contraseña — siempre pegado a la derecha del input
            btnViewPass.Location = new Point(
                textContra.Right + espacioBtnPass,
                yContra + altoLabel + 2
            );
            btnViewPass.Size = new Size(anchoBtnPass, altoInput);


            // --- BLOQUE DE BOTONES (SEGÚN TU DISEÑO CS) ---
            // Queremos: Guardar (Ancho completo) arriba
            //           Limpiar y Eliminar (Mitad y mitad) abajo

            int altoBotones = 48;
            int espacioEntreBotones = 10;
            // Calculamos desde el fondo hacia arriba
            int yFilaBotonesAbajo = h - altoBotones - 40;
            int yFilaBotonGuardar = yFilaBotonesAbajo - altoBotones - espacioEntreBotones;

            // 1. Botón Guardar (Ocupa todo el ancho disponible)
            btnGuardar.Location = new Point(margenGral, yFilaBotonGuardar);
            btnGuardar.Size = new Size(anchoInput, altoBotones);

            // 2. Botones de abajo (Mitad del ancho cada uno)
            int anchoBotonesPequenos = (anchoInput - espacioEntreBotones) / 2;

            btnLimpiar.Location = new Point(margenGral, yFilaBotonesAbajo);
            btnLimpiar.Size = new Size(anchoBotonesPequenos, altoBotones);

            btnEliminar.Location = new Point(margenGral + anchoBotonesPequenos + espacioEntreBotones, yFilaBotonesAbajo);
            btnEliminar.Size = new Size(anchoBotonesPequenos, altoBotones);

            pnlFormulario.Invalidate();
        }

        private void pnlBotones_Resize(object sender, EventArgs e)
        {
            int w = pnlBotones.Width;
            int h = pnlBotones.Height;

            int margenH = 15;
            int altoControl = 28;

            int espacioPequeño = 5;
            int espacioGrande = 20;

            // Cambia automáticamente a 2 líneas
            bool dosLineas = w < 750;

            if (!dosLineas)
            {
                // ── UNA LÍNEA ─────────────────────────────
                int y = (h - altoControl) / 2;

                // Tamaños reales de labels
                int anchoLblBuscar = lblBuscarID.PreferredWidth;
                int anchoBtnBuscar = 42;

                int anchoLblRol = lblCROL.PreferredWidth;
                int anchoLblSesion = lblCSESION.PreferredWidth;
                int anchoLblEstado = lblCESTADO.PreferredWidth;

                // Espacio disponible para inputs
                int espacioDisponible =
                    w
                    - (margenH * 2)
                    - anchoLblBuscar
                    - anchoBtnBuscar
                    - anchoLblRol
                    - anchoLblSesion
                    - anchoLblEstado
                    - (espacioPequeño * 6)
                    - (espacioGrande * 3);

                // Repartimos espacio
                int anchoBuscar = (int)(espacioDisponible * 0.40);
                int anchoCombo = (int)((espacioDisponible - anchoBuscar) / 3);

                // ── BUSCAR ──
                lblBuscarID.Location = new Point(margenH, y + 4);

                textBuscarU.Location =
                    new Point(lblBuscarID.Right + espacioPequeño, y);

                textBuscarU.Size =
                    new Size(anchoBuscar, altoControl);

                btnBuscarU.Location =
                    new Point(textBuscarU.Right + 2, y);

                btnBuscarU.Size =
                    new Size(anchoBtnBuscar, altoControl);

                // ── ROL ──
                lblCROL.Location =
                    new Point(btnBuscarU.Right + espacioGrande, y + 4);

                cbCROL.Location =
                    new Point(lblCROL.Right + espacioPequeño, y);

                cbCROL.Size =
                    new Size(anchoCombo, altoControl);

                // ── SESIÓN ──
                lblCSESION.Location =
                    new Point(cbCROL.Right + espacioGrande, y + 4);

                cbCSESION.Location =
                    new Point(lblCSESION.Right + espacioPequeño, y);

                cbCSESION.Size =
                    new Size(anchoCombo, altoControl);

                // ── ESTADO ──
                lblCESTADO.Location =
                    new Point(cbCSESION.Right + espacioGrande, y + 4);

                cbCESTADO.Location =
                    new Point(lblCESTADO.Right + espacioPequeño, y);

                cbCESTADO.Size =
                    new Size(
                        w - cbCESTADO.Left - margenH,
                        altoControl
                    );
            }
            else
            {
                // ── DOS LÍNEAS ────────────────────────────
                int margenV = 10;

                int anchoDisponible = w - (margenH * 2);

                int anchoBtnBuscar = 42;

                // ── FILA 1 : BUSCAR ──
                int y1 = margenV;

                lblBuscarID.Location =
                    new Point(margenH, y1 + 4);

                int anchoTxtBuscar =
                    anchoDisponible
                    - lblBuscarID.Width
                    - anchoBtnBuscar
                    - 10;

                textBuscarU.Location =
                    new Point(lblBuscarID.Right + espacioPequeño, y1);

                textBuscarU.Size =
                    new Size(anchoTxtBuscar, altoControl);

                btnBuscarU.Location =
                    new Point(textBuscarU.Right + 2, y1);

                btnBuscarU.Size =
                    new Size(anchoBtnBuscar, altoControl);

                // ── FILA 2 : COMBOS ──
                int y2 = y1 + altoControl + 10;

                int anchoGrupo =
                    (anchoDisponible - (espacioGrande * 2)) / 3;

                // ── ROL ──
                lblCROL.Location =
                    new Point(margenH, y2 + 4);

                cbCROL.Location =
                    new Point(lblCROL.Right + espacioPequeño, y2);

                cbCROL.Size =
                    new Size(
                        anchoGrupo - lblCROL.Width,
                        altoControl
                    );

                // ── SESIÓN ──
                lblCSESION.Location =
                    new Point(cbCROL.Right + espacioGrande, y2 + 4);

                cbCSESION.Location =
                    new Point(lblCSESION.Right + espacioPequeño, y2);

                cbCSESION.Size =
                    new Size(
                        anchoGrupo - lblCSESION.Width,
                        altoControl
                    );

                // ── ESTADO ──
                lblCESTADO.Location =
                    new Point(cbCSESION.Right + espacioGrande, y2 + 4);

                cbCESTADO.Location =
                    new Point(lblCESTADO.Right + espacioPequeño, y2);

                cbCESTADO.Size =
                    new Size(
                        w - cbCESTADO.Left - margenH,
                        altoControl
                    );
            }

            pnlBotones.Invalidate();
        }

        private void pnlBD_Resize(object sender, EventArgs e)
        {
            int w = pnlBD.Width;
            int h = pnlBD.Height;
            int margen = 10;

            dgvUsuariosDB.Location = new Point(margen, margen);
            dgvUsuariosDB.Size = new Size(w - margen * 2, h - margen * 2);

            pnlBD.Invalidate();
        }

        private void pnlUsuarios_Resize(object sender, EventArgs e)
        {
            int w = pnlUsuarios.Width;
            int h = pnlUsuarios.Height;
            int margen = 12;
            int espacioH = 10;

            int anchoIzq = (int)(w * 0.40) - margen;
            int anchoDer = w - anchoIzq - (margen * 3) - espacioH;

            // Alto del pnlBotones — más alto en pantalla pequeña para 2 filas
            int altoBotones = anchoDer < 600 ? 110 : 60;

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

            pnlUsuarios.Invalidate();
        }

        private void btnBuscarU_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnBuscarU, e, 15);
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnViewPass_Click(object sender, EventArgs e)
        {
            _MostrarContrasenia = !_MostrarContrasenia;

            if (_MostrarContrasenia)
            {
                textContra.PasswordChar = '*';
                btnViewPass.IconChar = FontAwesome.Sharp.IconChar.EyeLowVision;
            }
            else
            {
                textContra.PasswordChar = '\0';
                btnViewPass.IconChar = FontAwesome.Sharp.IconChar.Eye;
            }
        }

        private void btnViewPass_Paint(object sender, PaintEventArgs e)
        {
            RedondearBoton(btnViewPass, e, 15);
        }
    }
}
