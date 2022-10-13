using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace HW_FileXML
{
    internal class Program
    {

        static void WriteXml(ref Orders orders, string path)
        {
            XmlTextWriter writer = new XmlTextWriter(path, Encoding.Default);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("OrderList");

            for(int index = 0; index < orders.orders.Count; index++)
            {
                var item = orders.orders.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;

                writer.WriteStartElement("OrderID", itemValue.OrderId.ToString());
                writer.WriteElementString("Price", itemValue.OrderPrice.ToString());
                writer.WriteElementString("Quantity", itemValue.Quantity.ToString());
                writer.WriteElementString("Product name", itemKey.Name);
                writer.WriteElementString("Product manufacturer", itemKey.Manufacturer);
                writer.WriteElementString("Product type", itemKey.Type);
                writer.WriteElementString("Product ID", itemKey.Id.ToString());
                writer.WriteElementString("Date", itemValue.OrderDate);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();

        }

        public static void DeserializeObj(string fileName)
        {
            Console.WriteLine("Read stream");
            XmlSerializer serializer = new XmlSerializer(typeof(object));


        }

        public static void PrintNodes(XmlNode elem)
        {
            if(elem.ChildNodes.Count > 0)
            {
                Console.WriteLine($"{elem.Name,-10} {elem.InnerText}");
                foreach(XmlNode node in elem.ChildNodes)
                {
                    PrintNodes(node);
                }
            }
        }

        static void ReadXmlDocument(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(path);
            XmlElement root = xml.DocumentElement;
            PrintNodes(root);
        }

        static void ReadXml(string path)
        {
            XmlTextReader reader = new XmlTextReader(path);
            while (reader.ReadToFollowing("OrderId"))
            {
                reader.ReadToFollowing("Price");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Quantity");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Date");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Product name");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Product manufacturer");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Product type");
                Console.WriteLine($"{reader.ReadString()}");
                reader.ReadToFollowing("Product Id");
                Console.WriteLine($"{reader.ReadString()}");
            }
            reader.Close();
        }

        static void WriteXmlDocument(ref Orders orders, string path)
        {
            XmlDocument xml = new XmlDocument();

            XmlElement employeesNode = xml.CreateElement("OrderList");

            for(int index = 0; index < orders.orders.Count; index++)
            {
                var item = orders.orders.ElementAt(index);
                var itemKey = item.Key;
                var itemValue = item.Value;

                XmlElement employeeNode = xml.CreateElement("OrderID");
                XmlAttribute attr = xml.CreateAttribute("id");
                attr.Value = itemValue.OrderId.ToString();
                employeeNode.Attributes.Append(attr);
                employeesNode.AppendChild(employeeNode);


                XmlElement priceNode = xml.CreateElement("Price");
                priceNode.InnerText = itemValue.OrderPrice.ToString();
                XmlElement nameNode = xml.CreateElement("Name");
                nameNode.InnerText = itemKey.Name;
                XmlElement typeNode = xml.CreateElement("Type");
                typeNode.InnerText = itemKey.Type;
                XmlElement manNode = xml.CreateElement("Manufacurer");
                manNode.InnerText = itemKey.Manufacturer;

                employeeNode.AppendChild(priceNode);
                employeeNode.AppendChild(nameNode);
                employeeNode.AppendChild(typeNode);
                employeeNode.AppendChild(manNode);
            }
            xml.AppendChild(employeesNode);
            xml.Save(path);
        }

        static void Main()
        {
            Product product1 = new Product
            {
                Id = 1,
                Manufacturer = "Malaysia",
                Name = "Intel",
                Type = "Processor"
            };
            Order y = new Order
            {
                OrderId = 1,
                Quantity = 5,
                OrderPrice = 44.99,
                OrderDate = "10.01.2022"
            };

            Orders product2 = new Orders();
            product2.orders = new Dictionary<Product, Order>();
            product2.orders.Add(product1, y);

            product2.OutOrders();

            Product product3 = new Product
            {
                Id = 2,
                Manufacturer = "Vietnam",
                Name = "Samsung",
                Type = "Memory"
            };
            Order yx = new Order
            {
                OrderId = 2,
                Quantity = 100,
                OrderPrice = 999.99,
                OrderDate = "11.01.2022"
            };

            product2.orders.Add(product3, yx);
            product2.OutOrders();

            WriteXml(ref product2, "test.xml");
            ReadXml("test.xml");
            //DeserializeObject("test.xml");

            ReadXmlDocument("test.xml");
            WriteXmlDocument(ref product2, "test.xml");



        }
    }
}