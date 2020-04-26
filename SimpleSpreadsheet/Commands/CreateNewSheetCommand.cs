using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public class CreateNewSheetCommand : ICommand
    {
        private readonly int _width;
        private readonly int _height;

        public CreateNewSheetCommand(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Sheet Execute()
        {
            var sheet = Sheet.Create(_width, _height);
            return sheet;
        }
    }
}
