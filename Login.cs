using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Trabajo_1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ModernTheme.Apply(this); // Si usás tu theme
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Opcional: enfocar automáticamente en el campo usuario
            txtUsuario.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Debe ingresar usuario y contraseña", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using var conexion = new SqliteConnection("Data Source=sistema.db");
                conexion.Open();

                string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre = @usuario AND Password = @password";

                using var comando = new SqliteCommand(query, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@password", password);

                var resultado = Convert.ToInt32(comando.ExecuteScalar());

                if (resultado > 0)
                {
                    MessageBox.Show("Bienvenido " + usuario, "Login correcto",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir formulario principal
                    Form1 frm = new Form1();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
