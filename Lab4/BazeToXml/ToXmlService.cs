using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using AdventureWorks.Models;

namespace ToXmlService
{
    public class ToXml
    {
        private string path;
        private string filename;
        private string fullxmlpath;
        private string fullxsdpath;
        public ToXml(string sourcePath, string Name)
        {
            path = sourcePath;
            filename = Name;
        }
        public void XMLGenerate(SearchResult<SalesOrder>[] searchResults, SearchCriteria searchCriteria)
        {
            XDocument xdoc = new XDocument();
            XElement mainRoot = new XElement("SalesOrders");
            XElement[] childNodes = new XElement[searchCriteria.Count];
            for (int i = 0; i < searchCriteria.Count; i++)
            {
                childNodes[i] = new XElement($"SalesOreder{i + 1}");
                XElement salesOrderId = new XElement("SalesOrderId", searchResults[i].Entities.CustomerId);
                XElement customerId = new XElement("CustomerId", searchResults[i].Entities.CreditCardId);
                XElement territoryId = new XElement("TerritoryId", searchResults[i].Entities.TerritoryId);
                XElement creditCardId = new XElement("CreditCardId", searchResults[i].Entities.CreditCardId);
                XElement shipToAddressId = new XElement("ShipToAddressId", searchResults[i].Entities.ShipToAddressId);
                XElement accountNumber = new XElement("AccountNumber", searchResults[i].Entities.AccountNumber);
                XElement territoryName = new XElement("TerritoryName", searchResults[i].Entities.TerritoryName);
                XElement cardType = new XElement("CardType", searchResults[i].Entities.CardType);
                XElement city = new XElement("City", searchResults[i].Entities.City);

                childNodes[i].Add(salesOrderId);
                childNodes[i].Add(customerId);
                childNodes[i].Add(territoryId);
                childNodes[i].Add(creditCardId);
                childNodes[i].Add(shipToAddressId);
                childNodes[i].Add(accountNumber);
                childNodes[i].Add(territoryName);
                childNodes[i].Add(cardType);
                childNodes[i].Add(city);

                mainRoot.Add(childNodes[i]);
            }
            xdoc.Add(mainRoot);
            fullxmlpath = Path.Combine(path, filename + ".xml");
            xdoc.Save(fullxmlpath);
        }
        public string XSDGenerate(string xmlText)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xmlText));
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);
            string result = "";
            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    s.Write(memoryStream);
                    memoryStream.Position = 0;
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
            }
            return result;
        }
        public void SaveXsd()
        {
            string xmlText;
            if (File.Exists(fullxmlpath))
            {
                xmlText = File.ReadAllText(fullxmlpath);
            }
            else throw new InvalidOperationException("SalesOrders.xml not founded");
            fullxsdpath = Path.Combine(path, filename + ".xsd");
            using (StreamWriter sw = File.CreateText(fullxsdpath))
            {
                sw.Write(XSDGenerate(xmlText));
            }
        }
    }
}
