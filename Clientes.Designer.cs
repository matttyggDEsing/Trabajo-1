namespace Trabajo_1.clientes
{
    partial class Form2
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
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblCuit = new Label();
            txtCuit = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(100, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(180, 24);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Agregar Cliente";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(50, 70);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(47, 13);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(120, 67);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 20);
            txtNombre.TabIndex = 2;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Location = new Point(50, 110);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(35, 13);
            lblCuit.TabIndex = 3;
            lblCuit.Text = "CUIT:";
            // 
            // txtCuit
            // 
            txtCuit.Location = new Point(120, 107);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(200, 20);
            txtCuit.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(120, 150);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 30);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(220, 150);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 30);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 211);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtCuit);
            Controls.Add(lblCuit);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Clientes";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}