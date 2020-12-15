using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigManager
{
    public class StrEnter
    {
        public string Input { get; set; }
        public string Output { get; set; }

        public StrEnter GetConnectionString(string configPath)
        {
            XMLReader TryRecord1 = new XMLReader();
            JsonReader TryRecord2 = new JsonReader();
            StrEnter MainPath = new StrEnter();
                string typeOfFile = PathCheck(configPath);
                switch (typeOfFile)//создаём объект нужного конфига
                {
                case "xml":
                    MainPath = TryRecord1.GetOptions(configPath);
                    return MainPath;
                case "json":
                    MainPath = TryRecord2.GetOptions(configPath);
                    return MainPath;
                }
                throw new InvalidOperationException("Wrong path");
        }
        public string PathCheck(string path)//проверяем с чем мы работаем
        {
            int lenght = path.Length;
            if (lenght < 5)
            {
                throw new InvalidOperationException("Wrong path");
            }
            if (path[lenght - 1] == 'l' && path[lenght - 2] == 'm' && path[lenght - 3] == 'x' && path[lenght - 4] == '.')
            {
                return "xml";
            }
            else if (path[lenght - 1] == 'n' && path[lenght - 2] == 'o' && path[lenght - 3] == 's' && path[lenght - 4] == 'j' && path[lenght - 5] == '.')
            {
                return "json";
            }
            else
            {
                throw new InvalidOperationException("Wrong path");
            }
        }
    } 
}
