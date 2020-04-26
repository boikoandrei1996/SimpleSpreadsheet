using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public interface ICommand
    {
        Sheet Execute();
    }
}
