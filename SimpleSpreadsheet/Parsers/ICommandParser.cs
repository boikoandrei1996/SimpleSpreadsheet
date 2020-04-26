using SimpleSpreadsheet.Commands;

namespace SimpleSpreadsheet.Parsers
{
    public interface ICommandParser
    {
        (CommandType type, string[] args) Parse(string line, char separator);
    }
}
