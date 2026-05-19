using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using VeloxSoft.Config;
using VeloxSoft.Models;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormLogIn : Form
    {
        private readonly AutenticarUsuario _autenticarUsuario;
        private bool _MostrarContrasenia = false;
        public FormLogIn(AutenticarUsuario autenticarUsuario)
        {
            InitializeComponent();
            LabelSalir.BringToFront();
            _autenticarUsuario = autenticarUsuario;
            // Contraseña oculta al iniciar
            TxtPassword.UseSystemPasswordChar = false;
            // Icono inicial
            pbViewPassword.Image = Properties.Resources.Not_view;
        }

        private void NavPanel_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void LabelLimpiar_Click(object sender, EventArgs e)
        {
            TxtUsuario.Clear();
            TxtPassword.Clear();
            TxtUsuario.Focus();
            LabelError.Visible = false;
        }

        private void LabelSalir_Click(object sender, EventArgs e)
        {
            Application.ExitThread(); // Detiene el hilo actual
            Application.Exit();       // Envía la señal de cierre a la app
        }

        private void LabelSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            LabelError.Text = string.Empty;
            string errorMessage = null;
            string Id = TxtUsuario.Text;
            string Password = TxtPassword.Text;

            if (Id != string.Empty && Password != string.Empty)
            {
                usuario = _autenticarUsuario.Autenticar(Id, Password, out errorMessage);
            }


            if (Id.Length != 10)
            {
                LabelError.Text = "El ID debe tener exactamente 10 caracteres.";
                LabelError.Visible = true;
                return;
            }

            if (usuario.Nombre != "Error")
            {
                Program.UsuarioLogueado = usuario;
                Program.RolActual = ObtenerRolEnum.ObtenerRol(usuario.Rol);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                LabelError.Text = errorMessage;
                LabelError.Visible = true;
            }
        }

        private void NavPanel_Paint(object sender, PaintEventArgs e)
        {
            RedondearPanel((Panel)sender, e, 15);
        }

        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogInButton.PerformClick();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _MostrarContrasenia = !_MostrarContrasenia;

            if (_MostrarContrasenia)
            {
                TxtPassword.UseSystemPasswordChar = false;
                pbViewPassword.Image = Properties.Resources.Not_view;
            }
            else
            {
                TxtPassword.UseSystemPasswordChar = true;
                pbViewPassword.Image = Properties.Resources.View;
            }
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


    }

}
