using System;
using System.Windows.Forms;
using Trabajo_1.Modelos;
using Trabajo_1.DB;
namespace Trabajo_1
{
    public partial class Form1 : Form
    {
        private Factura facturaActual = new Factura
        {
            Cliente = new Cliente { Nombre = string.Empty, Cuit = string.Empty }
        };
        public Form1()
        {
            InitializeComponent();
            BaseDatos.Inicializar();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = txtProducto.Text,
                    Precio = decimal.Parse(txtPrecio.Text)
                };

                facturaActual.AgregarItem(producto, (int)nudCantidad.Value);

                lstProductos.Items.Add($"{producto.Nombre} - {nudCantidad.Value} x ${producto.Precio} = ${producto.Precio * nudCantidad.Value}");
                txtTotal.Text = facturaActual.Total.ToString("0.00");

                txtProducto.Clear();
                txtPrecio.Clear();
                nudCantidad.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (facturaActual.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.");
                return;
            }

            facturaActual.Cliente = new Cliente
            {
                Nombre = txtCliente.Text,
                Cuit = txtCuit.Text
            };

            MessageBox.Show($"Factura generada para {facturaActual.Cliente.Nombre}\nTotal: ${facturaActual.Total:0.00}");

            facturaActual = new Factura
            {
                Cliente = new Cliente { Nombre = string.Empty, Cuit = string.Empty }
            };
            lstProductos.Items.Clear();
            txtCliente.Clear();
            txtCuit.Clear();
            txtProducto.Clear();
            txtPrecio.Clear();
            txtTotal.Clear();
            nudCantidad.Value = 1;
        }

        private void lstProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica que deseas ejecutar cuando se seleccione un elemento en lstProductos.
            // Por ejemplo, mostrar información del producto seleccionado.
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();

            cmbClientes.DataSource = BaseDatos.ObtenerClientes();
            cmbClientes.DisplayMember = "Nombre";
            cmbClientes.ValueMember = "Cuit";
            cmbClientes.SelectedIndexChanged += (s, ev) =>
            {
                var clienteSeleccionado = cmbClientes.SelectedItem as Cliente;
                if (clienteSeleccionado != null)
                {
                    txtCliente.Text = clienteSeleccionado.Nombre;
                    txtCuit.Text = clienteSeleccionado.Cuit;
                }
            };

            cmbClientes.SelectedIndex = -1; // Para que no haya selección inicial
            txtCuit.ReadOnly = true; // Hacer que el campo Cuit sea de solo lectura

            txtCliente.TextChanged += (s, ev) =>
            {
                var clienteSeleccionado = cmbClientes.SelectedItem as Cliente;
                if (clienteSeleccionado != null)
                {
                    clienteSeleccionado.Nombre = txtCliente.Text;
                }
            };

            // Cargar productos en la lista de productos
            var productos = BaseDatos.ObtenerProductos();
            foreach (var producto in productos)
            {
                lstProductos.Items.Add($"{producto.Nombre} - ${producto.Precio}");
            }

            lstProductos.SelectedIndexChanged += lstProductos_SelectedIndexChanged;

            // Configurar el evento de cambio de texto en txtCliente
            txtCliente.TextChanged += txtCliente_TextChanged;

            // Configurar el evento de cambio de texto en txtCuit
            txtCuit.TextChanged += (s, ev) =>
            {
                var clienteSeleccionado = cmbClientes.SelectedItem as Cliente;
                if (clienteSeleccionado != null)
                {
                    clienteSeleccionado.Cuit = txtCuit.Text;
                }
            };
        }

     
    }
}