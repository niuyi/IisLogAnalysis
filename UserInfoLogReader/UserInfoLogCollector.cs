using IISLogReader;

namespace UserInfoLogReader
{
    public interface IUserInfoLogCollector
    {
        void Handle(UserInfoLogLine line);
        void Result();
    }
}