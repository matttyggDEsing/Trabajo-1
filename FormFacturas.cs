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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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

            // Seleccionar la fecha actual por defecto  
            dtpFecha.Value = DateTime.Today;

            // Mostrar facturas de hoy al cargar  
            CargarFacturas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarFacturas();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarFacturas();
        }

        private void CargarFacturas()
        {
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            string sql = @"
        SELECT 
            f.Id AS FacturaId,
            f.Fecha,
            c.Nombre AS Cliente,
            p.Nombre AS Producto,
            fp.Cantidad,
            p.Precio AS PrecioUnitario,
            (fp.Cantidad * p.Precio) AS Subtotal,
            ROW_NUMBER() OVER() AS RowId -- 🔹 Columna única para evitar conflicto
        FROM Facturas f
        JOIN Clientes c ON c.Id = f.ClienteId
        JOIN FacturaProductos fp ON fp.FacturaId = f.Id
        JOIN Productos p ON p.Id = fp.ProductoId
        WHERE date(f.Fecha) = date(@fecha)";

            if (cmbClientes.SelectedItem is ComboboxItem clienteSel)
                sql += " AND f.ClienteId = @clienteId";

            sql += " ORDER BY f.Fecha, f.Id;";

            using var cmd = new SqliteCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.ToString("yyyy-MM-dd"));
            if (cmbClientes.SelectedItem is ComboboxItem cliSel)
                cmd.Parameters.AddWithValue("@clienteId", cliSel.Value);

            using var reader = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.BeginLoadData();  // Evita validar mientras carga
            dt.Load(reader);
            dt.EndLoadData();    // Ahora no dará error porque las filas ya son únicas

            dgvFacturas.DataSource = dt;
        }




        private void dgvFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() => Text;
    }
}
