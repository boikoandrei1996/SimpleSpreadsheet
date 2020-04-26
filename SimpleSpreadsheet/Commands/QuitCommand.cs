using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public class QuitCommand : ICommand
    {
        public Sheet Execute()
        {
            throw new QuitProgramException();
        }
    }
}
