using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IISLogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"D:\IISLogReader\IISLogReader\IISLogReader\log\IIS logs", "*.log", SearchOption.AllDirectories);
            Console.WriteLine("Get files {0}", files.Count());
            Console.WriteLine("######################################################");
            foreach (var file in files)
            {
                ReadLogFile(file);
            }
            Console.ReadKey();
        }

        private static void ReadLogFile(string logFileName)
        {
            var dic = ReadLog(logFileName);
            Console.WriteLine("Finish Read {0}, Read User Count: {1}", logFileName, dic.Count);
            WriteToFile(dic);
            Console.WriteLine("Finish Write {0} ", logFileName);
            Console.WriteLine("######################################################");
        }

        private static void WriteToFile(Dictionary<string, IList<string>> dic)
        {
            foreach (var userInfo in dic)
            {
                var fileName = string.Format("../../Output/{0}.log", userInfo.Key);

                var streamWriter = GetStreamWriter(fileName);

                foreach (var newLine in userInfo.Value)
                {
                    streamWriter.WriteLine(newLine);
                }
                streamWriter.Close();
            }
        }

        private static Dictionary<string, IList<string>> ReadLog(string inputLogFileName)
        {
            var dic = new Dictionary<string, IList<string>>();
            var file = File.OpenText(inputLogFileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                HandleOneLine(line, dic);
            }
            file.Close();
            return dic;
        }

        private static void HandleOneLine(string line, Dictionary<string, IList<string>> dic)
        {
            var newLine = IisLogLine.GetIisLogLine(line);
            if(newLine == null)
                return;

            if(newLine.Url.Contains("Calendar"))
                return;

            if (newLine.Method == "GET" && !newLine.Url.Contains("TaxQuestionnaire")) 
                return;

            var sIp = newLine.Ip;
            if(!dic.ContainsKey(sIp))
            {
                dic[sIp] = new List<string>();
            }
            dic[sIp].Add(UserInfoLogLine.CreateFromIisLogLine(newLine).ToString());
        }

        private static StreamWriter GetStreamWriter(string fileName)
        {
            return !File.Exists(fileName) ? File.CreateText(fileName) : File.AppendText(fileName);
        }
    }
}
