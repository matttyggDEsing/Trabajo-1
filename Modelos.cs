using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabajo_1.Modelos
{
    public class Cliente
    {
        public required string Nombre { get; set; }
        public required string Cuit { get; set; }
    }

    public class Producto
    {
        public required string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

    public class ItemFactura
    {
        public required Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    }

    public class Factura
    {
        public required Cliente Cliente { get; set; }
        public List<ItemFactura> Items { get; private set; } = new List<ItemFactura>();

        public decimal Total => Items.Sum(i => i.Subtotal);

        public void AgregarItem(Producto producto, int cantidad)
        {
            Items.Add(new ItemFactura { Producto = producto, Cantidad = cantidad });
        }
    }
}