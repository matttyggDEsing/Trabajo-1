using Microsoft.Data.Sqlite;
using System;
using System.Windows.Forms;
using Trabajo_1.DB;

namespace Trabajo_1
{
    public partial class FormFacturas : Form
    {
        public FormFacturas()
        {
            InitializeComponent();
        }
        private void CargarClientes()
        {
            try
            {
                cmbClientes.Items.Clear();

                // 👉 Agregamos primero la opción "mostrar todas"
                cmbClientes.Items.Add(new
                {
                    Id = 0,
                    Nombre = "Mostrar todas las facturas",
                    Cuit = ""
                });

                using var conexion = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=sistema.db");
                conexion.Open();

                string sql = "SELECT Id, Nombre, Cuit FROM Clientes ORDER BY Nombre";

                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(sql, conexion);
                using var lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    cmbClientes.Items.Add(new
                    {
                        Id = lector.GetInt32(0),
                        Nombre = lector.GetString(1),
                        Cuit = lector.GetString(2)
                    });
                }

                cmbClientes.DisplayMember = "Nombre";
                cmbClientes.ValueMember = "Id";

                // 👉 dejamos seleccionada la opción "Mostrar todas" por defecto
                cmbClientes.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FormFacturas_Load(object sender, EventArgs e)
        {
            CargarFacturas();
            CargarClientes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarFacturas();
        }

        private void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var facturaId = Convert.ToInt64(dgvFacturas.Rows[e.RowIndex].Cells["FacturaId"].Value);
            new VerFactura(facturaId).ShowDialog();
        }
        private void CargarFacturas()
        {
            try
            {
                dgvFacturas.Rows.Clear();
                dgvFacturas.Columns.Clear();

                // Definir columnas del DataGridView
                dgvFacturas.Columns.Add("FacturaId", "Factura N°");
                dgvFacturas.Columns.Add("FechaFactura", "Fecha");
                dgvFacturas.Columns.Add("Cliente", "Cliente");
                dgvFacturas.Columns.Add("Producto", "Producto");
                dgvFacturas.Columns.Add("Cantidad", "Cantidad");
                dgvFacturas.Columns.Add("PrecioUnitario", "Precio Unit.");
                dgvFacturas.Columns.Add("Subtotal", "Subtotal");

                using var conexion = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=sistema.db");
                conexion.Open();

                string sql = @"
        SELECT 
            f.Id AS FacturaId,
            f.Fecha AS FechaFactura,
            c.Nombre AS Cliente,
            p.Nombre AS Producto,
            fp.Cantidad,
            p.Precio AS PrecioUnitario,
            (fp.Cantidad * p.Precio) AS Subtotal
        FROM Facturas f
        INNER JOIN Clientes c ON f.ClienteId = c.Id
        INNER JOIN FacturaProductos fp ON f.Id = fp.FacturaId
        INNER JOIN Productos p ON fp.ProductoId = p.Id
        WHERE 1=1
        ";

                // Filtro por fecha (solo si el check está marcado)
                if (chkFiltrarFecha.Checked)
                {
                    sql += " AND DATE(f.Fecha) = DATE(@fecha)";
                }

                // Filtro por cliente (solo si no es "Mostrar todas")
                if (cmbClientes.SelectedItem != null)
                {
                    dynamic clienteSel = cmbClientes.SelectedItem;
                    if (clienteSel.Id != 0)
                    {
                        sql += " AND f.ClienteId = @clienteId";
                    }
                }

                using var cmd = new Microsoft.Data.Sqlite.SqliteCommand(sql, conexion);

                // Asignar parámetros solo si corresponde
                if (chkFiltrarFecha.Checked)
                {
                    cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                }

                if (cmbClientes.SelectedItem != null)
                {
                    dynamic clienteSel = cmbClientes.SelectedItem;
                    if (clienteSel.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@clienteId", clienteSel.Id);
                    }

                }


                using var lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    dgvFacturas.Rows.Add(
                        lector["FacturaId"],
                        Convert.ToDateTime(lector["FechaFactura"]).ToString("dd/MM/yyyy"),
                        lector["Cliente"],
                        lector["Producto"],
                        lector["Cantidad"],
                        lector["PrecioUnitario"],
                        lector["Subtotal"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar facturas:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvFacturas_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dgvFacturas.CellDoubleClick += dgvFacturas_CellDoubleClick;
        }
    }
}
