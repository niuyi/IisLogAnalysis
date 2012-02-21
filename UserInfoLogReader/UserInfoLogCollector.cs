using System.Collections.Generic;
using IISLogReader;

namespace UserInfoLogReader
{
    public interface IUserInfoLogCollector
    {
        void Handle(string user, IEnumerable<UserInfoLogLine> lines);
        void Result();
    }
}