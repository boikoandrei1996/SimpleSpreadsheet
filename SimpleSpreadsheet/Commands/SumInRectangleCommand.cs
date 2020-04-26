using System;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Commands
{
    public class SumInRectangleCommand : ICommand
    {
        private readonly Sheet _sheet;
        private readonly int _x1;
        private readonly int _y1;
        private readonly int _x2;
        private readonly int _y2;
        private readonly int _x3;
        private readonly int _y3;

        public SumInRectangleCommand(
            Sheet sheet,
            int x1, int y1,
            int x2, int y2,
            int x3, int y3)
        {
            _sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _x3 = x3;
            _y3 = y3;
        }

        public Sheet Execute()
        {
            var minX = Math.Min(_x1, _x2);
            var maxX = Math.Max(_x1, _x2);

            var minY = Math.Min(_y1, _y2);
            var maxY = Math.Max(_y1, _y2);

            var sum = 0;
            for (int iX = minX; iX <= maxX; iX++)
            {
                for (int iY = minY; iY <= maxY; iY++)
                {
                    sum += _sheet.GetValue(iX, iY);
                }
            }

            _sheet.SetValue(_x3, _y3, sum);

            return _sheet;
        }
    }
}
