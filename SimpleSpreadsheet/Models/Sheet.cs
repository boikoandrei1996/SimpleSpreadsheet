using System;
using System.Text;
using SimpleSpreadsheet.Exceptions;

namespace SimpleSpreadsheet.Models
{
    public class Sheet
    {
        private readonly int[,] _values;

        public int Width { get; }
        public int Height { get; }

        private Sheet(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            _values = new int[width, height];
        }

        public static Sheet Create(int width, int height)
        {
            // ValidateSheetBounds
            if (width <= 0)
            {
                throw new ValidationException($"Invalid width {width} for sheet.");
            }

            if (height <= 0)
            {
                throw new ValidationException($"Invalid height {height} for sheet.");
            }

            return new Sheet(width, height);
        }

        public int GetValue(int x, int y)
        {
            this.ValidateIndex(x, y);
            return _values[x, y];
        }

        public void SetValue(int x, int y, int value)
        {
            this.ValidateIndex(x, y);
            _values[x, y] = value;
        }

        public override string ToString()
        {
            var capacity = (this.Width + 1) * 6 * (this.Height + 1) + this.Height;
            var builder = new StringBuilder(capacity);

            builder.Append("    |");
            for (int x = 0; x < this.Width; x++)
            {
                builder.AppendFormat(" {0,3} |", x);
            }

            for (int y = 0; y < this.Height; y++)
            {
                builder.Append(Environment.NewLine);
                builder.AppendFormat("{0,3} |", y);
                for (int x = 0; x < this.Width; x++)
                {
                    builder.AppendFormat(" {0,3} |", _values[x, y] == 0 ? "_" : _values[x, y].ToString());
                }
            }

            return builder.ToString();
        }

        private void ValidateIndex(int x, int y)
        {
            if (x < 0 || x >= this.Width)
            {
                throw new ValidationException($"Invalid index x={x} for sheet [{this.Width}, {this.Height}].");
            }

            if (y < 0 || y >= this.Height)
            {
                throw new ValidationException($"Invalid index y={y} for sheet [{this.Width}, {this.Height}].");
            }
        }
    }
}
