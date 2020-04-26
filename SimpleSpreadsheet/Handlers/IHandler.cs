using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Handlers
{
    public interface IHandler
    {
        Sheet CurrentSheet { get; }
        void Handle(string line);
    }
}
