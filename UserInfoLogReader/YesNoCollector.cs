using System;
using IISLogReader;

namespace UserInfoLogReader
{
    public class YesNoCollector : IUserInfoLogCollector
    {
        private readonly int count;
        private int yesCount = 0;
        private int noCount = 0;
        public YesNoCollector(int count)
        {
            this.count = count;
        }

        public void Handle(UserInfoLogLine line)
        {
            if (line.Url.Contains("QuestionAnswerYes"))
                yesCount++;

            if (line.Url.Contains("QuestionAnswerNo"))
                noCount++;
        }

        public void Result()
        {
            Console.WriteLine("Avg Yes: {0} , Avg No: {1}", yesCount/count, noCount/count);
        }
    }
}