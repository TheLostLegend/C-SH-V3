using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ConfigManager;
using AdventureWorks.Models;
using AdventureWorks.ServiceLayer;
using System.Data.SqlClient;
using ToXmlService;

namespace AdventureWorks.DataManagerSL
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string configPath = @"D:\config.xml";
            StrEnter MainPath = new StrEnter();
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
        }

        protected override void OnStop()
        {
        }
    }
}
