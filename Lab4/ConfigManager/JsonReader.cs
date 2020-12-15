using System;
using System.IO;
using Newtonsoft.Json;

namespace ConfigManager
{
    class JsonReader : IBase
    {

        private StrEnter restored = new StrEnter();

        public StrEnter GetOptions(string configPath)//метод для извлечения данных
        {
            if (System.IO.File.Exists(configPath))
            {

                // чтение данных
                JsonExtract(File.ReadAllText(configPath));
                using (StreamWriter sw = new StreamWriter(@"D:\templog.txt"))
                {
                    sw.WriteLine(restored.Input + " cool" + restored.Output);
                }
                    return this.restored;
            }
            else
            {
                return null;
            }
        }


        public void JsonExtract(string jsonString)
        {
            string bufferString;
            int length = jsonString.Length;
            for (int i = 0; i < length; i++)
            {
                bufferString = ReadWord(jsonString, ref i);
                switch (bufferString)
                {
                   
                    case "Input":
                                    if (jsonString[i] == ':')
                                    {
                                        string bufferOptions;
                                        bufferOptions = ReadWord(jsonString, ref i);
                                        this.restored.Input = bufferOptions;
                                    }
                                    break;
                      
                    case "Output":
                                    if (jsonString[i] == ':')
                                    {
                                        string bufferOptions;
                                        bufferOptions = ReadWord(jsonString, ref i);
                                        this.restored.Output = bufferOptions;
                                    }
                                    break;
                        
                }
            }
        }
        public static string ReadWord(string jsonString, ref int readPosition)
        {
            string bufferString;
            int subStringStartPosition;
            int subStringLength = 0;
            int length = jsonString.Length;
            while (jsonString[readPosition] != '"')
            {
                if (readPosition == length - 1)
                {
                    return "";
                }
                readPosition++;
            }
            readPosition++;
            subStringStartPosition = readPosition;
            while (jsonString[readPosition] != '"')
            {
                if (readPosition == length - 1)
                {
                    return "";
                }
                readPosition++;
                subStringLength++;
            }
            readPosition++;
            bufferString = jsonString.Substring(subStringStartPosition, subStringLength);
            return bufferString;
        }
    }

    
}
