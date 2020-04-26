using System;
using System.Linq;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Exceptions;

namespace SimpleSpreadsheet.Parsers
{
    public class CommandLineParser : ICommandParser
    {
        public (CommandType type, string[] args) Parse(string line, char separator)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new ValidationException("Input line should not be NullOrWhiteSpace.");
            }

            var lineParts = line
                .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            (CommandType type, string[] args) result;
            switch (lineParts[0])
            {
                case "c":
                    result = (CommandType.CreateNewSheet, lineParts.Skip(1).ToArray());
                    break;
                case "n":
                    result = (CommandType.InsertNumber, lineParts.Skip(1).ToArray());
                    break;
                case "s":
                    result = (CommandType.SumInRectangle, lineParts.Skip(1).ToArray());
                    break;
                case "q":
                    result = (CommandType.Quit, Array.Empty<string>());
                    break;
                default:
                    throw new CommandTypeException($"Not supported command type: {lineParts[0]}");
            }

            return result;
        }
    }
}
