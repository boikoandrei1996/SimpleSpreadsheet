using System;
using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Handlers;
using SimpleSpreadsheet.Parsers;
using SimpleSpreadsheet.Validators;

namespace SimpleSpreadsheet
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommandParser parser = new CommandLineParser();
            ICommandValidator validator = new CommandLineValidator();
            IHandler handler = new SimpleHandler(parser, validator);

            while (true)
            {
                Console.Write("enter command: ");
                var line = Console.ReadLine();

                try
                {
                    handler.Handle(line);
                    Console.WriteLine(handler.CurrentSheet?.ToString());
                }
                catch (CommandTypeException ex)
                {
                    Console.WriteLine("CommandType error: " + ex.Message);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine("Validation error: " + ex.Message);
                }
                catch (QuitProgramException)
                {
                    Console.WriteLine("Finished...");
                    break;
                }
            }

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
