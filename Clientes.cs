using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_1.clientes
{
    public partial class Form2 : Form  // Aquí corregí la herencia
    {
        public bool ClienteAgregado { get; private set; } = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCuit.Text))
            {
                MessageBox.Show("Debe completar todos los campos", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Guardar en la base de datos
            var cliente = new Trabajo_1.Modelos.Cliente
            {
                Nombre = txtNombre.Text.Trim(),
                Cuit = txtCuit.Text.Trim()
            };
            Trabajo_1.DB.BaseDatos.GuardarCliente(cliente);

            MessageBox.Show("Cliente guardado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar campos después de guardar
            txtNombre.Clear();
            txtCuit.Clear();
            ClienteAgregado = true;
            this.Close();
        }
      


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}