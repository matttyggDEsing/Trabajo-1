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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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


            txtCuit.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            txtTotal.ReadOnly = true;
        }





        // --- L�GICA DE CARGA Y LIMPIEZA ---

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

        // --- EVENTOS DE LOS CONTROLES (Ahora deber�an funcionar) ---

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

        // --- L�GICA DE BOTONES (Sin cambios) ---

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem == null) return;

            string producto = cmbProductos.Text;
            int cantidad = (int)nudCantidad.Value;
            decimal precio = decimal.Parse(txtPrecio.Text);
            decimal subtotal = cantidad * precio;

            dgvProductos.Rows.Add(producto, cantidad, precio, subtotal);

            CalcularTotal();
        }
        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }
            txtTotal.Text = total.ToString("0.00");
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var fila = dgvProductos.SelectedRows[0];

                // Pedimos nueva cantidad
                int cantidad = (int)nudCantidad.Value;
                decimal precio = decimal.Parse(txtPrecio.Text);
                decimal subtotal = cantidad * precio;

                fila.Cells["Cantidad"].Value = cantidad;
                fila.Cells["PrecioUnitario"].Value = precio;
                fila.Cells["Subtotal"].Value = subtotal;

                CalcularTotal();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para actualizar.", "Atenci�n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                dgvProductos.Rows.RemoveAt(dgvProductos.SelectedRows[0].Index);
                CalcularTotal();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para borrar.", "Atenci�n",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var formProductos = new Trabajo_1.productos.FormAgregarProducto();
            formProductos.ShowDialog();

            if (formProductos.ProductoAgregado)
            {
                CargarProductos();
                cmbProductos.SelectedIndex = cmbProductos.Items.Count - 1; // Seleccionar �ltimo
            }
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
                    MessageBox.Show("Factura guardada con �xito.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dgvProductos.Rows.Clear();
            decimal total = 0;
            foreach (var item in itemsFactura)
            {
                dgvProductos.Rows.Add($"{item.Cantidad} x {item.Producto.Nombre} - ${item.Subtotal:0.00}");
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
            dgvProductos.Rows.Clear();
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
            using (var formClientes = new Form2())
            {
                if (formClientes.ShowDialog() == DialogResult.OK)
                {
                    // refrescar lista de clientes si es necesario
                    CargarClientes();
                }
            }
            CargarClientes();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            var formFacturas = new FormFacturas();
            formFacturas.ShowDialog();
        }


    }
}