using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IISLogReader;

namespace UserInfoLogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"D:\IISLogReader\Output", "*.log", SearchOption.AllDirectories);
            Console.WriteLine("Get files {0}", files.Count());
            Console.WriteLine("***************************************************");

            var collectors = GetCollectors();
            foreach (var filePath in files)
            {
                ReadOneFile(filePath, collectors);
            }

            PrintResults(collectors);

            Console.ReadKey();
        }

        private static void PrintResults(IEnumerable<IUserInfoLogCollector> collectors)
        {
            foreach (var collector in collectors)
            {
                collector.Result();
            }
        }

        private static IEnumerable<IUserInfoLogCollector> GetCollectors()
        {
            var collectors = new List<IUserInfoLogCollector>();
            collectors.Add(new YesNoCollector());
            return collectors;
        }

        private static void ReadOneFile(string filePath, IEnumerable<IUserInfoLogCollector> collectors)
        {
            var file = File.OpenText(filePath);
            var userName = GetUserName(filePath);
            var userInfoLogLines = GetUserInfoLines(file);
            foreach (var collector in collectors)
            {
                collector.Handle(userName, userInfoLogLines);
            }
            file.Close();
        }

        private static string GetUserName(string filePath)
        {
            var name = new FileInfo(filePath).Name;
            return name.Substring(0, name.Length - 4);
        }

        private static IEnumerable<UserInfoLogLine> GetUserInfoLines(StreamReader file)
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                yield return UserInfoLogLine.CreateFromUserInfoLogFile(line);
            }
        }
    }
}
