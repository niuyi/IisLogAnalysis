using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IISLogReader;

namespace UserInfoLogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"D:\IISLogReader\IISLogReader\IISLogReader\Output", "*.log", SearchOption.AllDirectories);
            Console.WriteLine("Get files {0}", files.Count());
            Console.WriteLine("######################################################");

            IUserInfoLogCollector collector = new YesNoCollector(files.Count());
            foreach (var filePath in files)
            {
                ReadOneFile(filePath, collector);
            }

            collector.Result();
            Console.ReadKey();
        }

        private static void ReadOneFile(string filePath, IUserInfoLogCollector collector)
        {
            var file = File.OpenText(filePath);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var newline = UserInfoLogLine.CreateFromUserInfoLogFile(line);
                collector.Handle(newline);
            }
            file.Close();
        }
    }
}
