namespace Trabajo_1.productos
{
    partial class FormAgregarProducto
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblPrecio = new Label();
            txtNombre = new TextBox();
            txtPrecio = new TextBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(20, 60);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(43, 15);
            lblPrecio.TabIndex = 1;
            lblPrecio.Text = "Precio:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(90, 17);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(180, 23);
            txtNombre.TabIndex = 2;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(90, 57);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(180, 23);
            txtPrecio.TabIndex = 3;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(90, 100);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // FormAgregarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 150);
            Controls.Add(lblNombre);
            Controls.Add(lblPrecio);
            Controls.Add(txtNombre);
            Controls.Add(txtPrecio);
            Controls.Add(btnGuardar);
            Name = "FormAgregarProducto";
            Text = "Agregar Producto";
            Load += FormAgregarProducto_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
