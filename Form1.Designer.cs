namespace Trabajo_1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.Windows.Forms.ComboBox cmbClientes;

        private void InitializeComponent()
        {
            lblCliente = new Label();
            lblCuit = new Label();
            lblProducto = new Label();
            txtProducto = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            btnAgregar = new Button();
            lstProductos = new ListBox();
            lblTotal = new Label();
            txtTotal = new TextBox();
            txtCuit = new TextBox();
            btnFinalizar = new Button();
            cmbClientes = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(12, 20);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(44, 15);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente";
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Location = new Point(12, 55);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(32, 15);
            lblCuit.TabIndex = 2;
            lblCuit.Text = "CUIT";
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(12, 90);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(56, 15);
            lblProducto.TabIndex = 4;
            lblProducto.Text = "Producto";
            // 
            // txtProducto
            // 
            txtProducto.Location = new Point(80, 87);
            txtProducto.Name = "txtProducto";
            txtProducto.Size = new Size(300, 23);
            txtProducto.TabIndex = 5;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(12, 125);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(49, 15);
            lblPrecio.TabIndex = 6;
            lblPrecio.Text = "Precio $";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(80, 122);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(100, 23);
            txtPrecio.TabIndex = 7;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(200, 125);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 8;
            lblCantidad.Text = "Cantidad";
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(260, 122);
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(60, 23);
            nudCantidad.TabIndex = 9;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(350, 120);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(100, 27);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "AGREGAR";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lstProductos
            // 
            lstProductos.FormattingEnabled = true;
            lstProductos.ItemHeight = 15;
            lstProductos.Location = new Point(80, 160);
            lstProductos.Name = "lstProductos";
            lstProductos.Size = new Size(370, 154);
            lstProductos.TabIndex = 11;
            lstProductos.SelectedIndexChanged += lstProductos_SelectedIndexChanged;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(12, 330);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(43, 15);
            lblTotal.TabIndex = 12;
            lblTotal.Text = "TOTAL";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(80, 327);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(150, 23);
            txtTotal.TabIndex = 13;
            // 
            // txtCuit
            // 
            txtCuit.Location = new Point(80, 55);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(300, 23);
            txtCuit.TabIndex = 15;
            // 
            // btnFinalizar
            // 
            btnFinalizar.Location = new Point(80, 365);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(370, 30);
            btnFinalizar.TabIndex = 14;
            btnFinalizar.Text = "FINALIZAR FACTURA";
            btnFinalizar.UseVisualStyleBackColor = true;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // cmbClientes
            // 
            cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientes.FormattingEnabled = true;
            cmbClientes.Location = new Point(80, 17);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(300, 23);
            cmbClientes.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 420);
            Controls.Add(cmbClientes);
            Controls.Add(btnFinalizar);
            Controls.Add(txtTotal);
            Controls.Add(lblTotal);
            Controls.Add(lstProductos);
            Controls.Add(btnAgregar);
            Controls.Add(nudCantidad);
            Controls.Add(lblCantidad);
            Controls.Add(txtPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(txtProducto);
            Controls.Add(lblProducto);
            Controls.Add(lblCuit);
            Controls.Add(lblCliente);
            Controls.Add(txtCuit);
            Name = "Form1";
            Text = "Facturación Productos";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ListBox lstProductos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnFinalizar;
    }
}