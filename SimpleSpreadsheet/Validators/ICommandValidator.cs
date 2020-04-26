using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Validators
{
    public interface ICommandValidator
    {
        void Validate(CommandType type, string[] args, Sheet currentSheet);
    }
}
