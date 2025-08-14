using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Trabajo_1
{
    public partial class FormFacturas : Form
    {
        public FormFacturas()
        {
            InitializeComponent();
        }

        private void FormFacturas_Load(object sender, EventArgs e)
        {
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            // Cargar clientes
            using var cmdClientes = new SqliteCommand("SELECT Id, Nombre FROM Clientes", conexion);
            using var readerClientes = cmdClientes.ExecuteReader();
            while (readerClientes.Read())
            {
                cmbClientes.Items.Add(new ComboboxItem
                {
                    Text = readerClientes.GetString(1),
                    Value = readerClientes.GetInt64(0)
                });
            }
            readerClientes.Close();

            // Cargar productos
            using var cmdProductos = new SqliteCommand("SELECT Id, Nombre FROM Productos", conexion);
            using var readerProductos = cmdProductos.ExecuteReader();
            while (readerProductos.Read())
            {
                cmbProductos.Items.Add(new ComboboxItem
                {
                    Text = readerProductos.GetString(1),
                    Value = readerProductos.GetInt64(0)
                });
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedItem is ComboboxItem cliente)
            {
                CargarFacturasPorCampo("ClienteId", cliente.Value);
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is ComboboxItem producto)
            {
                CargarFacturasPorProducto(producto.Value);
            }
        }

        private void CargarFacturasPorCampo(string campo, object valor)
        {
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            string sql = $@"
                SELECT f.Id, c.Nombre AS Cliente, f.Fecha
                FROM Facturas f
                JOIN Clientes c ON c.Id = f.ClienteId
                WHERE f.{campo} = @valor
                ORDER BY f.Fecha DESC";

            using var cmd = new SqliteCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@valor", valor);

            using var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);

            dgvFacturas.DataSource = dt;
        }

        private void CargarFacturasPorProducto(object productoId)
        {
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            // Necesita la tabla intermedia FacturaProductos para que esto funcione
            string sql = @"
                SELECT f.Id, c.Nombre AS Cliente, f.Fecha
                FROM Facturas f
                JOIN Clientes c ON c.Id = f.ClienteId
                JOIN FacturaProductos fp ON fp.FacturaId = f.Id
                WHERE fp.ProductoId = @prodId
                ORDER BY f.Fecha DESC";

            using var cmd = new SqliteCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@prodId", productoId);

            using var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);

            dgvFacturas.DataSource = dt;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() => Text;
    }
}
