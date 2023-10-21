using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Producto
    {
        public Producto() { }
        public Producto(int id, string name, string descp, int price, int qty) 
        {
            this.ID = id;
            this.Name = name;
            this.Descp = descp;
            this.Price = price;
            this.Qty = qty;
        }
        public Producto(string name, string descp, int price, int qty)
        {
            this.Name = name;
            this.Descp = descp;
            this.Price = price;
            this.Qty = qty;
        }

        public int ID;
        public string Name;
        public string Descp;
        public int Price;
        public int Qty;
    }
}
