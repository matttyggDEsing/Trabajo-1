using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Trabajo_1.productos
{
    public partial class FormAgregarProducto : Form
    {
        public bool ProductoAgregado { get; private set; } = false;
        public FormAgregarProducto()
        {
            InitializeComponent();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var cmd = new SqliteCommand(
                "INSERT INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio)", conexion
            );
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Precio", precio);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Producto agregado con éxito.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void FormAgregarProducto_Load(object sender, EventArgs e)
        {

        }
    }
}
