using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Trabajo_1.Modelos;
using SQLitePCL;

namespace Trabajo_1.DB
{
    public static class BaseDatos
    {
        public static void Inicializar()
        {
            try
            {
                // Inicialización de SQLite
                SQLitePCL.Batteries.Init();

                // Ruta absoluta para la base de datos
                string dbPath = Path.Combine(Application.StartupPath, "sistema.db");
                bool dbExisted = File.Exists(dbPath);

                // Cadena de conexión
                var connectionString = $"Data Source={dbPath};";

                using var conexion = new SqliteConnection(connectionString);
                conexion.Open();

                // Comandos SQL para crear tablas
                var comandos = new[]
                {
            """
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL UNIQUE,
                Cuit TEXT NOT NULL UNIQUE
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS Productos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL UNIQUE,
                Precio REAL NOT NULL CHECK(Precio >= 0)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS Facturas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ClienteId INTEGER NOT NULL,
                Fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
                FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
            );
            """
        };

                // Ejecutar comandos
                foreach (var sql in comandos)
                {
                    using var cmd = new SqliteCommand(sql, conexion);
                    cmd.ExecuteNonQuery();
                }

                // Mensaje informativo
                if (!dbExisted)
                {
                    MessageBox.Show($"Base de datos creada en: {dbPath}", "Información",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la base de datos:\n{ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var comando = new SqliteCommand("SELECT Nombre, MAX(Precio) as Precio FROM Productos GROUP BY Nombre", conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                productos.Add(new Producto
                {
                    Nombre = lector.GetString(0),
                    Precio = lector.GetDecimal(1)
                });
            }
            return productos;
        }

        public static List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var comando = new SqliteCommand("SELECT Id, Nombre, Cuit FROM Clientes", conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                clientes.Add(new Cliente
                {
                    Nombre = lector.GetString(1),
                    Cuit = lector.GetString(2)
                });
            }
            return clientes;
        }

        public static void GuardarFactura(Factura factura)
        {
            string dbPath = Path.Combine(Application.StartupPath, "sistema.db");
            var connectionString = $"Data Source={dbPath};";

            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();

            using var transaccion = conexion.BeginTransaction();
            try
            {
                // 1. Insertar el cliente solo si NO existe (por CUIT)
                var comandoCliente = new SqliteCommand(
                    "INSERT OR IGNORE INTO Clientes (Nombre, Cuit) VALUES (@Nombre, @Cuit);",
                    conexion, transaccion
                );
                comandoCliente.Parameters.AddWithValue("@Nombre", factura.Cliente.Nombre);
                comandoCliente.Parameters.AddWithValue("@Cuit", factura.Cliente.Cuit);
                comandoCliente.ExecuteNonQuery();

                // 2. Obtener el ID del cliente por CUIT
                var comandoGetId = new SqliteCommand(
                    "SELECT Id FROM Clientes WHERE Cuit = @Cuit;",
                    conexion, transaccion
                );
                comandoGetId.Parameters.AddWithValue("@Cuit", factura.Cliente.Cuit);
                var clienteIdObj = comandoGetId.ExecuteScalar();
                if (clienteIdObj is null)
                    throw new Exception("No se pudo obtener el Id del cliente.");

                var clienteId = Convert.ToInt64(clienteIdObj);

                // 3. Insertar la factura (solo con ClienteId)
                var comandoFactura = new SqliteCommand(
                    "INSERT INTO Facturas (ClienteId) VALUES (@ClienteId);",
                    conexion, transaccion
                );
                comandoFactura.Parameters.AddWithValue("@ClienteId", clienteId);
                comandoFactura.ExecuteNonQuery();

                // 4. Obtener el ID de la factura recién insertada
                var comandoFacturaId = new SqliteCommand(
                    "SELECT last_insert_rowid();",
                    conexion, transaccion
                );
                var facturaIdObj = comandoFacturaId.ExecuteScalar();
                if (facturaIdObj is null)
                    throw new Exception("No se pudo obtener el Id de la factura.");

                var facturaId = Convert.ToInt64(facturaIdObj);

                // 5. Insertar los productos y los ítems de la factura
                foreach (var item in factura.Items)
                {
                    // Insertar o ignorar el producto
                    var comandoProducto = new SqliteCommand(
                        "INSERT OR IGNORE INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio);",
                        conexion, transaccion
                    );
                    comandoProducto.Parameters.AddWithValue("@Nombre", item.Producto.Nombre);
                    comandoProducto.Parameters.AddWithValue("@Precio", item.Producto.Precio);
                    comandoProducto.ExecuteNonQuery();

                    // Obtener el ID del producto
                    var comandoProductoId = new SqliteCommand(
                        "SELECT Id FROM Productos WHERE Nombre = @Nombre;",
                        conexion, transaccion
                    );
                    comandoProductoId.Parameters.AddWithValue("@Nombre", item.Producto.Nombre);
                    var productoIdObj = comandoProductoId.ExecuteScalar();
                    if (productoIdObj is null)
                        throw new Exception("No se pudo obtener el Id del producto.");

                    var productoId = Convert.ToInt64(productoIdObj);

                    // Insertar el ítem de la factura
                    var comandoItem = new SqliteCommand(
                        "INSERT INTO FacturaItems (FacturaId, ProductoId, Cantidad, PrecioUnitario) " +
                        "VALUES (@FacturaId, @ProductoId, @Cantidad, @PrecioUnitario);",
                        conexion, transaccion
                    );
                    comandoItem.Parameters.AddWithValue("@FacturaId", facturaId);
                    comandoItem.Parameters.AddWithValue("@ProductoId", productoId);
                    comandoItem.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                    comandoItem.Parameters.AddWithValue("@PrecioUnitario", item.Producto.Precio);
                    comandoItem.ExecuteNonQuery();
                }

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public static void GuardarCliente(Cliente cliente)
        {
            string dbPath = Path.Combine(Application.StartupPath, "sistema.db");
            var connectionString = $"Data Source={dbPath};";
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();

            using var comando = new SqliteCommand(
                "INSERT OR IGNORE INTO Clientes (Nombre, Cuit) VALUES (@Nombre, @Cuit);", conexion);
            comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            comando.Parameters.AddWithValue("@Cuit", cliente.Cuit);
            comando.ExecuteNonQuery();
        }
    }
}
