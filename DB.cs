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
            SQLitePCL.Batteries.Init(); // Inicializa SQLite

            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            var comandos = new[]
            {
        """
        CREATE TABLE IF NOT EXISTS Clientes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nombre TEXT NOT NULL,
            Cuit TEXT NOT NULL
        );
        """,
        """
        CREATE TABLE IF NOT EXISTS Productos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nombre TEXT NOT NULL,
            Precio REAL NOT NULL
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

            foreach (var sql in comandos)
            {
                using var cmd = new SqliteCommand(sql, conexion);
                cmd.ExecuteNonQuery();
            }
        }


        public static List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var comando = new SqliteCommand("SELECT * FROM Productos", conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                productos.Add(new Producto
                {
                    Nombre = lector.GetString(1),
                    Precio = lector.GetDecimal(2)
                });
            }
            return productos;
        }

        public static List<Cliente> ObtenerClientes()
        {
            var clientes = new List<Cliente>();
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var comando = new SqliteCommand("SELECT * FROM Clientes", conexion);
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
            using var conexion = new SqliteConnection("Data Source=sistema.db");
            conexion.Open();

            using var transaccion = conexion.BeginTransaction();
            try
            {
                // Insertar cliente si no existe
                var comandoCliente = new SqliteCommand(
                    "INSERT OR IGNORE INTO Clientes (Nombre, Cuit) VALUES (@Nombre, @Cuit);",
                    conexion, transaccion
                );
                comandoCliente.Parameters.AddWithValue("@Nombre", factura.Cliente.Nombre);
                comandoCliente.Parameters.AddWithValue("@Cuit", factura.Cliente.Cuit);
                comandoCliente.ExecuteNonQuery();

                // Obtener Id del cliente
                comandoCliente = new SqliteCommand(
                    "SELECT Id FROM Clientes WHERE Cuit = @Cuit;",
                    conexion, transaccion
                );
                comandoCliente.Parameters.AddWithValue("@Cuit", factura.Cliente.Cuit);
                var clienteIdObj = comandoCliente.ExecuteScalar();
                if (clienteIdObj is null)
                    throw new Exception("No se pudo obtener el Id del cliente.");

                var clienteId = Convert.ToInt64(clienteIdObj);

                // Insertar factura
                var comandoFactura = new SqliteCommand(
                    "INSERT INTO Facturas (ClienteId) VALUES (@ClienteId);",
                    conexion, transaccion
                );
                comandoFactura.Parameters.AddWithValue("@ClienteId", clienteId);
                comandoFactura.ExecuteNonQuery();

                // Obtener Id de la factura
                comandoFactura = new SqliteCommand(
                    "SELECT last_insert_rowid();",
                    conexion, transaccion
                );
                var facturaIdObj = comandoFactura.ExecuteScalar();
                if (facturaIdObj is null)
                    throw new Exception("No se pudo obtener el Id de la factura.");

                var facturaId = Convert.ToInt64(facturaIdObj);

                // Insertar productos y relacionarlos con la factura
                foreach (var item in factura.Items)
                {
                    var comandoProducto = new SqliteCommand(
                        "INSERT OR IGNORE INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio);",
                        conexion, transaccion
                    );
                    comandoProducto.Parameters.AddWithValue("@Nombre", item.Producto.Nombre);
                    comandoProducto.Parameters.AddWithValue("@Precio", item.Producto.Precio);
                    comandoProducto.ExecuteNonQuery();

                    comandoProducto = new SqliteCommand(
                        "SELECT Id FROM Productos WHERE Nombre = @Nombre;",
                        conexion, transaccion
                    );
                    comandoProducto.Parameters.AddWithValue("@Nombre", item.Producto.Nombre);
                    var productoIdObj = comandoProducto.ExecuteScalar();
                    if (productoIdObj is null)
                        throw new Exception("No se pudo obtener el Id del producto.");

                    var productoId = Convert.ToInt64(productoIdObj);

                    // Aquí podrías insertar en una tabla intermedia FacturaProductos si la tuvieras
                }

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
    }
}
