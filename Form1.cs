using Trabajo_1.DB;
using Trabajo_1.Modelos;
using Trabajo_1.clientes;
namespace Trabajo_1
{
    public partial class Form1 : Form
    {
        private List<ItemFactura> itemsFactura = new List<ItemFactura>();

        // 1. CONSTRUCTOR DEL FORMULARIO
        public Form1()
        {
            InitializeComponent();
            Trabajo_1.clientes.Form2 clientesForm = new Trabajo_1.clientes.Form2();

            this.Load += new System.EventHandler(this.Form1_Load);

            this.cmbClientes.SelectedIndexChanged += new System.EventHandler(this.cmbClientes_SelectedIndexChanged);
            this.cmbProductos.SelectedIndexChanged += new System.EventHandler(this.cmbProducto_SelectedIndexChanged);
            this.AgregarCliente.Click += new System.EventHandler(this.AgregarCliente_Click);
        }

        // 2. EVENTO DE CARGA DEL FORMULARIO
        private void Form1_Load(object sender, EventArgs e)
        {
            BaseDatos.Inicializar();
            LimpiarFormulario(); // Usamos LimpiarFormulario para la carga inicial
        }

        // --- LÓGICA DE CARGA Y LIMPIEZA ---

        private void CargarClientes()
        {
            this.cmbClientes.SelectedIndexChanged -= this.cmbClientes_SelectedIndexChanged;

            cmbClientes.DataSource = null;
            var clientes = BaseDatos.ObtenerClientes();
            // Mostrar "Nombre (CUIT)" en el ComboBox
            cmbClientes.DataSource = clientes;
            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "Cuit";

            this.cmbClientes.SelectedIndexChanged += this.cmbClientes_SelectedIndexChanged;
        }

        private void CargarProductos()
        {
            this.cmbProductos.SelectedIndexChanged -= this.cmbProducto_SelectedIndexChanged;

            cmbProductos.DataSource = null;
            var productos = BaseDatos.ObtenerProductos()
                .GroupBy(p => p.Nombre)
                .Select(g => g.First())
                .ToList();
            cmbProductos.DataSource = productos;
            cmbProductos.DisplayMember = "Nombre";
            cmbProductos.ValueMember = "Precio";

            this.cmbProductos.SelectedIndexChanged += this.cmbProducto_SelectedIndexChanged;
        }

        // --- EVENTOS DE LOS CONTROLES (Ahora deberían funcionar) ---

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedItem is Cliente clienteSeleccionado)
            {
                txtCuit.Text = clienteSeleccionado.Cuit;
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is Producto productoSeleccionado)
            {
                txtPrecio.Text = productoSeleccionado.Precio.ToString("0.00");
            }
        }

        // --- LÓGICA DE BOTONES (Sin cambios) ---

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is not Producto productoSeleccionado)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var item = new ItemFactura { Producto = productoSeleccionado, Cantidad = (int)nudCantidad.Value };
            itemsFactura.Add(item);
            ActualizarListaYTotal();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            // Obtener el cliente seleccionado correctamente
            Cliente clienteSeleccionado = cmbClientes.SelectedItem as Cliente;

            if (clienteSeleccionado != null)
            {
                if (!itemsFactura.Any())
                {
                    MessageBox.Show("No hay productos en la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var factura = new Factura { Cliente = clienteSeleccionado };
                foreach (var item in itemsFactura)
                {
                    factura.AgregarItem(item.Producto, item.Cantidad);
                }

                try
                {
                    BaseDatos.GuardarFactura(factura);
                    MessageBox.Show("Factura guardada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar la factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarListaYTotal()
        {
            lstProductos.Items.Clear();
            decimal total = 0;
            foreach (var item in itemsFactura)
            {
                lstProductos.Items.Add($"{item.Cantidad} x {item.Producto.Nombre} - ${item.Subtotal:0.00}");
                total += item.Subtotal;
            }
            txtTotal.Text = total.ToString("C");
        }

        private void LimpiarFormulario()
        {
            CargarClientes();
            CargarProductos();

            cmbClientes.SelectedIndex = -1;
            cmbProductos.SelectedIndex = -1;
            txtCuit.Clear();
            txtPrecio.Clear();
            nudCantidad.Value = 1;
            itemsFactura.Clear();
            lstProductos.Items.Clear();
            txtTotal.Clear();
        }

        private void lstProductos_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbClientes_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (cmbClientes.SelectedItem is Cliente clienteSeleccionado)
            {
                txtCuit.Text = clienteSeleccionado.Cuit;
            }
        }
        private void cmbProductos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is Producto productoSeleccionado)
            {
                txtPrecio.Text = productoSeleccionado.Precio.ToString("0.00");
            }
        }

        private void AgregarCliente_Click(object sender, EventArgs e)
        {
            var formClientes = new Trabajo_1.clientes.Form2();
            formClientes.ShowDialog();

            if (formClientes.ClienteAgregado)
            {
                CargarClientes(); // Solo refresca si realmente se agregó algo
            }
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            var formFacturas = new FormFacturas();
            formFacturas.ShowDialog();
        }


    }
}