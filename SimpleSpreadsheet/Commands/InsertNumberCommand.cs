using System;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public class InsertNumberCommand : ICommand
    {
        private readonly Sheet _sheet;
        private readonly int _x;
        private readonly int _y;
        private readonly int _value;

        public InsertNumberCommand(Sheet sheet, int x, int y, int value)
        {
            _sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));
            _x = x;
            _y = y;
            _value = value;
        }

        public Sheet Execute()
        {
            _sheet.SetValue(_x, _y, _value);
            return _sheet;
        }
    }
}
