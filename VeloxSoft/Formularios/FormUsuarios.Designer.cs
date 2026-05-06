namespace VeloxSoft.Formularios
{
    partial class FormUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlUsuarios = new Panel();
            pnlFormulario = new Panel();
            pnlBD = new Panel();
            lblTituloForm = new Label();
            pnlUsuarios.SuspendLayout();
            pnlFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // pnlUsuarios
            // 
            pnlUsuarios.Controls.Add(pnlBD);
            pnlUsuarios.Controls.Add(pnlFormulario);
            pnlUsuarios.Dock = DockStyle.Fill;
            pnlUsuarios.Location = new Point(0, 0);
            pnlUsuarios.Name = "pnlUsuarios";
            pnlUsuarios.Size = new Size(1362, 697);
            pnlUsuarios.TabIndex = 0;
            // 
            // pnlFormulario
            // 
            pnlFormulario.BackColor = SystemColors.ControlDark;
            pnlFormulario.Controls.Add(lblTituloForm);
            pnlFormulario.Location = new Point(12, 12);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(539, 673);
            pnlFormulario.TabIndex = 0;
            // 
            // pnlBD
            // 
            pnlBD.BackColor = SystemColors.GradientActiveCaption;
            pnlBD.Location = new Point(557, 12);
            pnlBD.Name = "pnlBD";
            pnlBD.Size = new Size(793, 673);
            pnlBD.TabIndex = 1;
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Franklin Gothic Medium Cond", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloForm.Location = new Point(17, 17);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(162, 51);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Usuarios";
            // 
            // FormUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1362, 697);
            Controls.Add(pnlUsuarios);
            Name = "FormUsuarios";
            Text = "FormUsuarios";
            pnlUsuarios.ResumeLayout(false);
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlUsuarios;
        private Panel pnlBD;
        private Panel pnlFormulario;
        private Label lblTituloForm;
    }
}