using System;
using System.Collections.Generic;
using System.Linq;
using IISLogReader;

namespace UserInfoLogReader
{
    public class YesNoCollector : IUserInfoLogCollector
    {
        private int yesCount = 0;
        private int noCount = 0;
        private readonly HashSet<string> users = new HashSet<string>();

        private void HandleOneLine(string user, UserInfoLogLine line)
        {
            if (line.Url.Contains("QuestionAnswerYes"))
            {
                yesCount++;
                users.Add(user);
            }


            if (line.Url.Contains("QuestionAnswerNo"))
            {
                noCount++;
                users.Add(user);
            }

        }

        public void Handle(string user, IEnumerable<UserInfoLogLine> lines)
        {
            var yes = yesCount;
            var no = noCount;
            foreach (var userInfoLogLine in lines)
            {
                HandleOneLine(user, userInfoLogLine);
            }

            Console.WriteLine("Yes:{0},NO:{1},User:{2}", yesCount - yes, noCount - no, user);
        }

        public void Result()
        {
            var i = users.Count;
            Console.WriteLine("Yes No Collector result." + i);
            Console.WriteLine("Avg Yes: {0} , Avg No: {1}", yesCount / i, noCount / i);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}