namespace VeloxSoft.Formularios
{
    partial class FormCorte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlCorte = new Panel();
            pnlAccion = new Panel();
            pnlTabla = new Panel();
            lblTitulo = new Label();
            lblInfo = new Label();
            lblResultado = new Label();
            btnCorte = new Button();
            dgCorte = new DataGridView();

            pnlCorte.SuspendLayout();
            pnlAccion.SuspendLayout();
            pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCorte).BeginInit();
            SuspendLayout();

            // ── pnlCorte ──
            pnlCorte.Dock = DockStyle.Fill;
            pnlCorte.BackColor = Color.FromArgb(245, 247, 242);
            pnlCorte.Controls.Add(pnlTabla);
            pnlCorte.Controls.Add(pnlAccion);
            pnlCorte.Name = "pnlCorte";
            pnlCorte.TabIndex = 0;
            pnlCorte.Resize += pnlCorte_Resize;

            // ── pnlAccion ──
            pnlAccion.BackColor = Color.White;
            pnlAccion.Controls.Add(lblTitulo);
            pnlAccion.Controls.Add(lblInfo);
            pnlAccion.Controls.Add(lblResultado);
            pnlAccion.Controls.Add(btnCorte);
            pnlAccion.Name = "pnlAccion";
            pnlAccion.TabIndex = 0;
            pnlAccion.Paint += pnlAccion_Paint;

            // ── pnlTabla ──
            pnlTabla.BackColor = Color.White;
            pnlTabla.Controls.Add(dgCorte);
            pnlTabla.Name = "pnlTabla";
            pnlTabla.TabIndex = 1;
            pnlTabla.Paint += pnlTabla_Paint;

            // ── lblTitulo ──
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Franklin Gothic Medium Cond", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.FromArgb(59, 109, 17);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Corte";

            // ── lblInfo ──
            lblInfo.AutoSize = false;
            lblInfo.Font = new Font("Century Gothic", 11F);
            lblInfo.ForeColor = Color.FromArgb(100, 100, 100);
            lblInfo.Name = "lblInfo";
            lblInfo.TabIndex = 1;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Text = "Al realizar el corte se calcularán\ntodas las ventas y gastos pendientes.";

            // ── lblResultado ──
            lblResultado.AutoSize = false;
            lblResultado.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            lblResultado.Name = "lblResultado";
            lblResultado.TabIndex = 2;
            lblResultado.TextAlign = ContentAlignment.MiddleCenter;
            lblResultado.Text = "";
            lblResultado.Visible = false;

            // ── btnCorte ──
            btnCorte.BackColor = Color.FromArgb(59, 109, 17);
            btnCorte.Cursor = Cursors.Hand;
            btnCorte.FlatAppearance.BorderSize = 0;
            btnCorte.FlatStyle = FlatStyle.Flat;
            btnCorte.Font = new Font("Century Gothic", 13F, FontStyle.Bold);
            btnCorte.ForeColor = Color.White;
            btnCorte.Name = "btnCorte";
            btnCorte.TabIndex = 3;
            btnCorte.Text = "Realizar Corte";
            btnCorte.UseVisualStyleBackColor = false;
            btnCorte.Click += btnCorte_Click;
            btnCorte.Paint += btnCorte_Paint;

            // ── dgCorte ──
            dgCorte.BackgroundColor = Color.White;
            dgCorte.BorderStyle = BorderStyle.None;
            dgCorte.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgCorte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgCorte.ColumnHeadersHeight = 40;
            dgCorte.EnableHeadersVisualStyles = false;
            dgCorte.AllowUserToAddRows = false;
            dgCorte.AllowUserToDeleteRows = false;
            dgCorte.ReadOnly = true;
            dgCorte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCorte.RowHeadersVisible = false;
            dgCorte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgCorte.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgCorte.Dock = DockStyle.Fill;
            dgCorte.Name = "dgCorte";
            dgCorte.TabIndex = 0;

            // ── FormCorte ──
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 650);
            Controls.Add(pnlCorte);
            Name = "FormCorte";
            Text = "FormCorte";

            pnlCorte.ResumeLayout(false);
            pnlAccion.ResumeLayout(false);
            pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgCorte).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCorte;
        private Panel pnlAccion;
        private Panel pnlTabla;
        private Label lblTitulo;
        private Label lblInfo;
        private Label lblResultado;
        private Button btnCorte;
        private DataGridView dgCorte;
    }
}