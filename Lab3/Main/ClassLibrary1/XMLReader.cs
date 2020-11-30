using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ConfigManager
{
    class XMLReader
    {
        private StrEnter restored = new StrEnter();

        public StrEnter GetOptions()//метод для извлечения данных
        {
            XmlDocument xDoc = new XmlDocument();
            int k = 0;
                if ((File.Exists("D:\\config.xml")) && (File.Exists("D:\\config.xsd")))
                {
                    XmlSchemaSet schemas = new XmlSchemaSet();
                    schemas.Add(null, @"D:\\config.xsd");

                    XDocument document = XDocument.Load(@"D:\\config.xml");

                    document.Validate(schemas, (sender, validationEventArgs) =>
                    {
                        k = 1;
                    });
                }
            if ((File.Exists("D:\\config.xml")) && (k == 0))
                {
                    xDoc.Load("D:\\config.xml");
                    // получим корневой элемент
                    XmlElement xRoot = xDoc.DocumentElement;
                    // обход всех узлов в корневом элементе
                    foreach (XmlNode xnode in xRoot)
                    {
                        // получаем атрибут 
                        if (xnode.Attributes.Count > 0)
                        {
                            XmlNode attr = xnode.Attributes.GetNamedItem("name");
                        }
                        // обходим все дочерние узлы элемента 
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            // если узел 
                            if (childnode.Name == "input")
                            {
                                restored.Input = childnode.InnerText;
                            }
                            // если узел
                            if (childnode.Name == "output")
                            {
                                restored.Output = childnode.InnerText;
                            }
                        }
                    }
                
                if (Directory.Exists(restored.Input) && Directory.Exists(restored.Output))
                {
                    return this.restored;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }

    
}
