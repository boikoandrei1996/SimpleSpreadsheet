using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Models;
using SimpleSpreadsheet.Parsers;
using SimpleSpreadsheet.Validators;

namespace SimpleSpreadsheet.Handlers
{
    public class SimpleHandler : IHandler
    {
        private readonly ICommandParser _parser;
        private readonly ICommandValidator _validator;

        public Sheet CurrentSheet { get; private set; }

        public SimpleHandler(ICommandParser parser, ICommandValidator validator)
        {
            _parser = parser;
            _validator = validator;
            this.CurrentSheet = null;
        }

        public void Handle(string line)
        {
            var (type, args) = _parser.Parse(line, ' ');
            _validator.Validate(type, args, this.CurrentSheet);
            var command = CommandFactory.Create(type, args, this.CurrentSheet);
            this.CurrentSheet = command.Execute();
        }
    }
}
