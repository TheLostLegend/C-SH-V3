using System;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ConfigManager
{
    public class Logger
    {
       
        StrEnter MainPath = new StrEnter();
        XMLReader TryRecord1 = new XMLReader();
        JsonReader TryRecord2 = new JsonReader();
        FileSystemWatcher watcher;
        string S = @"D:\UL\C#V3\Detroit";
        string W = @"D:\UL\C#V3\Human";
        object obj = new object();
        bool enabled = true;
        
        public Logger()
        {
            MainPath = TryRecord1.GetOptions();
            if (MainPath == null)
            {
                MainPath = TryRecord2.GetOptions();

            }
            if (MainPath != null)
            {
                S = MainPath.Input;
                W = MainPath.Output;
                
            }
            watcher = new FileSystemWatcher(S);
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }
        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // создание файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            filePath = e.Name;
            string bet1 = W + "\\" + "kd" + ".gz";
            string bet = W + "\\" + filePath;
            Compress(e.FullPath, bet1);
            FileInfo fileInf = new FileInfo(bet1);
            if (fileInf.Exists)
            {
                Decompress(bet1, bet);
            }

        }
        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }

        private static void Compress(string sourceFile, string compressedFile)
        {
            string text;
            using(StreamReader sr = new StreamReader(sourceFile))
            {
                text = sr.ReadToEnd();
            }




            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        using (StreamWriter sw = new StreamWriter(compressionStream))
                        {
                            sw.Write(Encript(text));
                        }
                        
                    }
                }
            }
        }

        private static void Decompress(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
            string text;
            using (StreamReader sr = new StreamReader(targetFile))
            {
                text = sr.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(targetFile))
            {
                sw.Write(Decript(text));
            }
        }

        private static string Encript(string str)
        {
            string newstr = "";
            for (int i=0, len = str.Length; i<len; i++)
            {
                newstr += Convert.ToChar(str[i] + 1);
            }

            return newstr;
        }

        private static string Decript(string str)
        {
            string newstr = "";
            for (int i = 0, len = str.Length; i < len; i++)
            {
                newstr += Convert.ToChar(str[i] - 1);
            }

            return newstr;
        }
    }
}