using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductoDAL
    {
        public bool GuardarProducto(Producto prod)
        {
            string query = $"" +
                $"INSERT INTO Productos(name, descp, price, qty) " +
                $"VALUES('{prod.Name}', '{prod.Descp}', '{prod.Price}', '{prod.Qty}')";
            int rows = DatabaseHelper.Instance.ExecuteNonQuery(query);
            if (rows == 0)
            {
                return false;
            }
            return true;
        }
        public List<Producto> ObtenerProductos()
        {
            string query = "SELECT * FROM Productos";
            DataTable dt = DatabaseHelper.Instance.ExecuteQuery(query);
            List<Producto> productos = new List<Producto>();
            foreach (DataRow row in dt.Rows)
            {
                Producto p = new Producto();
                p.ID = int.Parse(row["id"].ToString());
                p.Name = row["name"].ToString();
                p.Descp = row["descp"].ToString();
                p.Price = int.Parse(row["price"].ToString());
                p.Qty = int.Parse(row["qty"].ToString());
                productos.Add(p);
            }
            return productos;

        }

        public bool EliminarProducto(int id)
        {
            string query = $"DELETE FROM Productos WHERE id = {id}";
            int rows = DatabaseHelper.Instance.ExecuteNonQuery(query);
            return rows > 0;

        }

        public bool EditarProducto(Producto prod)
        {
            string query = $"UPDATE Productos " +
                $"SET name = '{prod.Name}', descp = '{prod.Descp}', price = {prod.Price}, qty = {prod.Qty} " +
                $"WHERE id = {prod.ID}";
            int rows = DatabaseHelper.Instance.ExecuteNonQuery(query);
            if (rows == 0)
            {
                return false;
            }
            return true;
        }
    }

}
