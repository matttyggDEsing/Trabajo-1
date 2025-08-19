using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace Trabajo_1
{
    public partial class VerFactura : Form
    {
        private readonly long _facturaId;

        public VerFactura(long facturaId)
        {
            InitializeComponent();
            _facturaId = facturaId;
        }

        private void VerFactura_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            using var con = new SqliteConnection("Data Source=sistema.db");
            con.Open();

            var cmd = new SqliteCommand(@"
                SELECT f.Id, c.Nombre, c.Cuit, f.Fecha
                FROM Facturas f
                JOIN Clientes c ON c.Id = f.ClienteId
                WHERE f.Id = @id", con);
            cmd.Parameters.AddWithValue("@id", _facturaId);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) { MessageBox.Show("Factura no encontrada"); Close(); return; }

            lblNumero.Text = r.GetInt64(0).ToString();
            lblCliente.Text = r.GetString(1);
            lblCuit.Text = r.GetString(2);
            lblFecha.Text = r.GetDateTime(3).ToString("dd/MM/yyyy");

            // Detalle
            dgvDetalle.Rows.Clear();
            dgvDetalle.Columns.Clear();
            dgvDetalle.Columns.Add("Producto", "Producto");
            dgvDetalle.Columns.Add("Cantidad", "Cantidad");
            dgvDetalle.Columns.Add("Precio", "Precio Unitario");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal");

            decimal total = 0;

            var cmdDet = new SqliteCommand(@"
                SELECT p.Nombre, fp.Cantidad, p.Precio, (fp.Cantidad * p.Precio) AS Sub
                FROM FacturaProductos fp
                JOIN Productos p ON p.Id = fp.ProductoId
                WHERE fp.FacturaId = @id", con);
            cmdDet.Parameters.AddWithValue("@id", _facturaId);

            using var rDet = cmdDet.ExecuteReader();
            while (rDet.Read())
            {
                string producto = rDet.GetString(0);
                int cantidad = rDet.GetInt32(1);
                decimal precio = rDet.GetDecimal(2);
                decimal subtotal = rDet.GetDecimal(3);
                total += subtotal;

                dgvDetalle.Rows.Add(producto, cantidad, precio.ToString("N2"), subtotal.ToString("N2"));
            }

            lblTotal.Text = $"Total: ${total:N2}";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarFacturaConCoordenadas();
        }

        private void GenerarFacturaConCoordenadas()
        {
            using var con = new SqliteConnection("Data Source=sistema.db");
            con.Open();

            // Datos encabezado
            var cmd = new SqliteCommand(@"
                SELECT f.Id, c.Nombre, c.Cuit, f.Fecha
                FROM Facturas f
                JOIN Clientes c ON c.Id = f.ClienteId
                WHERE f.Id = @id", con);
            cmd.Parameters.AddWithValue("@id", _facturaId);

            string id = "", nombre = "", cuit = "", fecha = "";
            using (var r = cmd.ExecuteReader()) if (r.Read())
                {
                    id = r.GetInt64(0).ToString();
                    nombre = r.GetString(1);
                    cuit = r.GetString(2);
                    fecha = r.GetDateTime(3).ToString("dd/MM/yyyy");
                }

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using var baseStream = assembly.GetManifestResourceStream("Trabajo_1.Factura_Base.pdf");
            if (baseStream == null)
            {
                MessageBox.Show("No se encontró el PDF base embebido.");
                return;
            }

            var inputPdf = PdfReader.Open(baseStream, PdfDocumentOpenMode.Import);
            var outputPdf = new PdfDocument();
            outputPdf.Version = inputPdf.Version;
            var page = outputPdf.AddPage(inputPdf.Pages[0]);
            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Arial", 10);
            var bold = new XFont("Arial", 10, XFontStyleEx.Bold);
            var brush = XBrushes.Black;

            // Encabezado
            gfx.DrawString(id, font, brush, 506, 71);
            gfx.DrawString(nombre, font, brush, 82, 192);
            gfx.DrawString(cuit, font, brush, 82, 205);          // +5
            gfx.DrawString(fecha, font, brush, 510, 93);

            // Detalle (30 pt por línea)
            int yProducto = 333;
            int yPrecio = 333;
            decimal total = 0;

            var cmdDet = new SqliteCommand(@"
                SELECT p.Nombre, fp.Cantidad, p.Precio, (fp.Cantidad * p.Precio) AS Sub
                FROM FacturaProductos fp
                JOIN Productos p ON p.Id = fp.ProductoId
                WHERE fp.FacturaId = @id", con);
            cmdDet.Parameters.AddWithValue("@id", _facturaId);

            int index = 0;
            using var rDet = cmdDet.ExecuteReader();
            while (rDet.Read())
            {
                string producto = rDet.GetString(0);
                int cantidad = rDet.GetInt32(1);
                decimal precio = rDet.GetDecimal(2);
                decimal subtotal = rDet.GetDecimal(3);
                total += subtotal;

                int offsetY = index * 30;      // 30 pt por línea

                gfx.DrawString(producto, font, brush, 133, yProducto + offsetY);
                gfx.DrawString(cantidad.ToString(), font, brush, 340, yProducto + offsetY);
                gfx.DrawString(precio.ToString("N2"), font, brush, 430, yPrecio + offsetY);
                gfx.DrawString(subtotal.ToString("N2"), font, brush, 493, yProducto + offsetY);

                index++;
            }

            // Total final
            gfx.DrawString(total.ToString("N2"), bold, brush, 454, 574);

            var outputPath = Path.Combine(Path.GetTempPath(), $"Factura_{_facturaId}.pdf");
            outputPdf.Save(outputPath);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = outputPath,
                UseShellExecute = true
            });
        }
    }
}