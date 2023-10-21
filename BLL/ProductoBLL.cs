using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ProductoBLL
    {
        private ProductoDAL productoDAL;
        public ProductoBLL()
        {
            productoDAL = new ProductoDAL();
        }

        public void AgregarProducto(Producto prod)
        {
            prod.Name = prod.Name.TrimEnd();
            prod.Descp = prod.Descp.TrimEnd();

            if (string.IsNullOrEmpty(prod.Name)) throw new Exception("El nombre no puede ir vacio");
            if (string.IsNullOrEmpty(prod.Descp)) throw new Exception("La descripción no puede ir vacia");
            if (prod.Price <= 0) throw new Exception("El precio debe ser mayor a cero");
            if (prod.Qty < 0) throw new Exception("La cantidad debe ser mayor a cero");

            Producto productoExiste = productoDAL.ObtenerProductos().Where(p => p.Name.Equals(prod.Name)).FirstOrDefault();
            if (productoExiste != null) throw new Exception("El nombre del producto ya existe");
            if (!productoDAL.GuardarProducto(prod)) throw new Exception("No se ha podido guardar el producto");

        }

        public List<Producto> ObtenerProductos()
        {
            return productoDAL.ObtenerProductos();
        }

        public void EliminarProducto(int id)
        {
            if (!productoDAL.EliminarProducto(id))
            {
                throw new Exception("No se ha podido eliminar el producto");
            }
        }

        public void EditarProducto(Producto prod)
        {
            if(!productoDAL.EditarProducto(prod))
            {
                throw new Exception("No se ha podido modificar el producto");
            }
        }
    }
}
