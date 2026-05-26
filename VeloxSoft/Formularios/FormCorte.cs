using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VeloxSoft.Models;
using VeloxSoft.Services;

namespace VeloxSoft.Formularios
{
    public partial class FormCorte : Form
    {
        private readonly ServicioCorte _ServicioCorte;

        public FormCorte(ServicioCorte servicioCorte)
        {
            _ServicioCorte = servicioCorte;
            InitializeComponent();
            this.DoubleBuffered = true;
            ConfigurarDGV();
            CargarCortes();
        }

        // ── LÓGICA ──────────────────────────────────────────

        private void CargarCortes()
        {
            var lista = _ServicioCorte.Ver_Cortes(out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MostrarResultado(errorMessage, false);
                return;
            }

            dgCorte.Rows.Clear();
            foreach (var corte in lista)
            {
                int i = dgCorte.Rows.Add(
                    corte.IdCorte,
                    corte.Fecha,
                    $"${corte.TotalVentas:N2}",
                    $"${corte.TotalGasto:N2}",
                    $"${corte.Ganancia:N2}");

                var celda = dgCorte.Rows[i].Cells["Ganancia"];
                celda.Style.ForeColor = corte.Ganancia >= 0
                    ? Color.FromArgb(39, 120, 10)
                    : Color.FromArgb(163, 45, 45);
                celda.Style.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            }
        }

        private void btnCorte_Click(object sender, EventArgs e)
        {
            btnCorte.Enabled = false;
            btnCorte.Text = "Calculando...";

            string mensaje = _ServicioCorte.Realizar_Corte(out string errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
                MostrarResultado(errorMessage, false);
            else
            {
                MostrarResultado(mensaje, true);
                CargarCortes();
            }

            btnCorte.Enabled = true;
            btnCorte.Text = "Realizar Corte";
        }

        private void MostrarResultado(string mensaje, bool exito)
        {
            lblResultado.Text = mensaje;
            lblResultado.ForeColor = exito
                ? Color.FromArgb(39, 120, 10)
                : Color.FromArgb(163, 45, 45);
            lblResultado.Visible = true;
        }

        // ── CONFIGURAR DGV ──────────────────────────────────

        private void ConfigurarDGV()
        {
            dgCorte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(59, 109, 17);
            dgCorte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgCorte.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            dgCorte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgCorte.DefaultCellStyle.Font = new Font("Century Gothic", 10F);
            dgCorte.DefaultCellStyle.ForeColor = Color.FromArgb(50, 50, 50);
            dgCorte.DefaultCellStyle.BackColor = Color.White;
            dgCorte.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 200);
            dgCorte.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 70, 10);
            dgCorte.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgCorte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 243, 222);
            dgCorte.GridColor = Color.FromArgb(220, 220, 220);

            dgCorte.Columns.Clear();
            dgCorte.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCorte", HeaderText = "ID", FillWeight = 8 });
            dgCorte.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "FECHA", FillWeight = 20 });
            dgCorte.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalVentas", HeaderText = "VENTAS", FillWeight = 24 });
            dgCorte.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalGasto", HeaderText = "GASTOS", FillWeight = 24 });
            dgCorte.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ganancia", HeaderText = "GANANCIA", FillWeight = 24 });
        }

        // ── RESIZE ──────────────────────────────────────────

        private void pnlCorte_Resize(object sender, EventArgs e)
        {
            int w = pnlCorte.Width;
            int h = pnlCorte.Height;
            int margen = 12;
            int espacio = 10;

            int anchoIzq = (int)(w * 0.28) - margen;
            int anchoDer = w - anchoIzq - (margen * 3) - espacio;

            pnlAccion.Location = new Point(margen, margen);
            pnlAccion.Size = new Size(anchoIzq, h - margen * 2);

            pnlTabla.Location = new Point(anchoIzq + margen * 2, margen);
            pnlTabla.Size = new Size(anchoDer, h - margen * 2);

            int margenI = 30;

            lblTitulo.Location = new Point(margenI - 10, 20);

            lblInfo.Location = new Point(margenI, 80);
            lblInfo.Size = new Size(pnlAccion.Width - margenI * 2, 60);

            btnCorte.Location = new Point(margenI, pnlAccion.Height / 2 - 26);
            btnCorte.Size = new Size(pnlAccion.Width - margenI * 2, 52);

            lblResultado.Location = new Point(margenI, btnCorte.Bottom + 15);
            lblResultado.Size = new Size(pnlAccion.Width - margenI * 2, 50);

            pnlAccion.Invalidate();
            pnlTabla.Invalidate();
        }

        // ── DIBUJO ──────────────────────────────────────────

        private void pnlAccion_Paint(object sender, PaintEventArgs e)
            => RedondearPanel(pnlAccion, e, 15);

        private void pnlTabla_Paint(object sender, PaintEventArgs e)
            => RedondearPanel(pnlTabla, e, 15);

        private void btnCorte_Paint(object sender, PaintEventArgs e)
            => RedondearBoton(btnCorte, e, 15);

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
    }
}