namespace Trabajo_1
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnCancelar;

        /// <summary>
        /// Limpiar recursos usados.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblUsuario = new Label();
            lblPassword = new Label();
            txtUsuario = new TextBox();
            txtPassword = new TextBox();
            btnIngresar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(120, 30);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(140, 24);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Iniciar Sesión";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(50, 90);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(50, 15);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(50, 135);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Contraseña:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(140, 87);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(180, 23);
            txtUsuario.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(140, 132);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(180, 23);
            txtPassword.TabIndex = 4;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(80, 200);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(100, 35);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(200, 200);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 35);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 280);
            Controls.Add(lblTitulo);
            Controls.Add(lblUsuario);
            Controls.Add(lblPassword);
            Controls.Add(txtUsuario);
            Controls.Add(txtPassword);
            Controls.Add(btnIngresar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
