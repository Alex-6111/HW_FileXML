using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_FileXML
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }

        public override string ToString()
        {
            return $"Type -> {Type} |  Name -> {Name} | Manufacturer -> {Manufacturer} | ID -> {Id}";
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            return $"Order Id -> {OrderId} | Quantity -> {Quantity} | Order date -> {OrderDate} | Order price -> {OrderPrice}$";
        }
    }

    public class Orders
    {
        public Dictionary<Product, Order> orders { get; set; }

        public void OutOrders()
        {
            foreach(KeyValuePair<Product, Order> keyValue in orders)
            {
                Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
            }
            Console.WriteLine();
        }
    }
}
