namespace Trabajo_1
{
    partial class FormFacturas
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbClientes;
        private ComboBox cmbProductos;
        private Button btnBuscarCliente;
        private Button btnBuscarProducto;
        private DataGridView dgvFacturas;

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
            cmbClientes = new ComboBox();
            cmbProductos = new ComboBox();
            btnBuscarCliente = new Button();
            btnBuscarProducto = new Button();
            dgvFacturas = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();

            cmbClientes.Location = new Point(20, 20);
            cmbClientes.Size = new Size(200, 23);

            btnBuscarCliente.Location = new Point(230, 20);
            btnBuscarCliente.Size = new Size(100, 23);
            btnBuscarCliente.Text = "Por Cliente";
            btnBuscarCliente.Click += btnBuscarCliente_Click;

            cmbProductos.Location = new Point(20, 60);
            cmbProductos.Size = new Size(200, 23);

            btnBuscarProducto.Location = new Point(230, 60);
            btnBuscarProducto.Size = new Size(100, 23);
            btnBuscarProducto.Text = "Por Producto";
            btnBuscarProducto.Click += btnBuscarProducto_Click;

            dgvFacturas.Location = new Point(20, 100);
            dgvFacturas.Size = new Size(500, 300);
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToAddRows = false;

            ClientSize = new Size(550, 420);
            Controls.Add(cmbClientes);
            Controls.Add(btnBuscarCliente);
            Controls.Add(cmbProductos);
            Controls.Add(btnBuscarProducto);
            Controls.Add(dgvFacturas);

            Text = "Facturas";
            Load += FormFacturas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
        }
    }
}
