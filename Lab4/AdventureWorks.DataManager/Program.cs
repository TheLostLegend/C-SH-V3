using System;
using ConfigManager;
using AdventureWorks.Models;
using AdventureWorks.ServiceLayer;
using System.Data.SqlClient;
using ToXmlService;

namespace AdventureWorks.DataManager
{
    class DataManager
    {
        static void Main(string[] args)
        {
            
            string configPath = @"D:\config.xml";
            StrEnter MainPath= new StrEnter();
            MainPath = MainPath.GetConnectionString(configPath);
            string connectionString = MainPath.Input;
            Console.WriteLine(connectionString);
            XMLCriteria searchCriteria = new XMLCriteria();
            searchCriteria.StartId = 43659;
            searchCriteria.Count = 3;

            ArrayDataXML ordersProvider = new ArrayDataXML(connectionString);
            var salesOrders = ordersProvider.GetOrders(searchCriteria);
            ToXml generator = new ToXml(MainPath.Output, "SalesOrders");
            generator.XMLGenerate(salesOrders, searchCriteria);
            generator.SaveXsd();

            Console.WriteLine("Подключение закрыто...");
            Console.ReadKey();
        }
    }
}