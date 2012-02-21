using System;

namespace IISLogReader
{
    public class UserInfoLogLine
    {
        public static UserInfoLogLine CreateFromIisLogLine(IisLogLine iisLogLine)
        {
            return new UserInfoLogLine
                       {
                           Date = iisLogLine.Date,
                           Time = iisLogLine.Time,
                           Method = iisLogLine.Method,
                           Url = iisLogLine.Url,
                           Query = iisLogLine.Query
                       };
        }

        private UserInfoLogLine()
        {
        }

        public string Date { get; private set; }
        public string Time { get; private set; }
        public string Method { get; private set; }
        public string Url { get; private set; }
        public string Query { get; private set; }

        public new string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Date, Time, Method, Url, Query);
        }

        public static UserInfoLogLine CreateFromUserInfoLogFile(string line)
        {
            var words = line.Split(' ');
            return new UserInfoLogLine()
                       {
                           Date = words[0], 
                           Time = words[1], 
                           Method = words[2], 
                           Url = words[3], 
                           Query = words[4]
                       };
        }
    }
}