using System;
using System.Linq;
using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public static class CommandFactory
    {
        public static ICommand Create(CommandType type, string[] args, Sheet sheet)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            int[] values = args.Select(x => int.Parse(x)).ToArray();

            ICommand result;
            switch (type)
            {
                case CommandType.CreateNewSheet:
                    result = new CreateNewSheetCommand(values[0], values[1]);
                    break;

                case CommandType.InsertNumber:
                    result = new InsertNumberCommand(sheet, values[0], values[1], values[2]);
                    break;

                case CommandType.SumInRectangle:
                    result = new SumInRectangleCommand(
                        sheet,
                        values[0], values[1],
                        values[2], values[3],
                        values[4], values[5]);
                    break;

                case CommandType.Quit:
                    result = new QuitCommand();
                    break;

                default:
                    throw new CommandTypeException($"Not supported command type: {type}");
            }

            return result;
        }
    }
}
