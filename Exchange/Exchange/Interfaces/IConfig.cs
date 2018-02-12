
using SQLite.Net.Interop;

namespace Exchange.Interfaces
{
    public interface IConfig
    {
        string DirectoryDB { get;  }

        ISQLitePlatform Platform { get; }
    }
}
