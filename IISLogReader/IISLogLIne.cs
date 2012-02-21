namespace IISLogReader
{
    public class IisLogLine
    {
        public static IisLogLine GetIisLogLine(string line)
        {
            var words = line.Split(' ');
            if (words.Length != 22)
                return null;

            var iisLogLine = new IisLogLine
                                 {
                                     Date = words[0],
                                     Time = words[1],
                                     Method = words[5],
                                     Url = words[6],
                                     Query = words[7],
                                     Ip =words[10]
                                 };

            return iisLogLine;
        }
        private IisLogLine()
        {
        }

        public string Date { get; private set; } 
        public string Time { get; private set; } 
        public string Method { get; private set; } 
        public string Url { get; private set; } 
        public string Query { get; private set; } 
        public string Ip { get; private set; } 
    }
}