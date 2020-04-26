using System;
using System.Collections.Generic;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Validators
{
    public class CommandLineValidator : ICommandValidator
    {
        public void Validate(CommandType type, string[] args, Sheet currentSheet)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            switch (type)
            {
                case CommandType.CreateNewSheet:
                    this.ValidateCreateNewSheetCommand(args);
                    break;

                case CommandType.InsertNumber:
                    this.ValidateInsertNumberCommand(args, currentSheet);
                    break;

                case CommandType.SumInRectangle:
                    this.ValidateSumInRectangleCommand(args, currentSheet);
                    break;

                case CommandType.Quit:
                    break;

                default:
                    throw new CommandTypeException($"Not supported command type: {type}");
            }
        }

        private void ValidateCreateNewSheetCommand(string[] args)
        {
            this.ValidateArgsShouldHaveCount(args, 2);

            var values = this.ValidateAllArgsShouldBeNumeric(args);
            this.ValidateValueShouldBePositive(values[0]);
            this.ValidateValueShouldBePositive(values[1]);
        }

        private void ValidateInsertNumberCommand(string[] args, Sheet currentSheet)
        {
            this.ValidateArgsShouldHaveCount(args, 3);

            var values = this.ValidateAllArgsShouldBeNumeric(args);
            var x1 = values[0];
            var y1 = values[1];

            this.ValidateSheetShouldExist(currentSheet);
            this.ValidateCellIndexShouldBeInBounds(currentSheet, x1, y1);
        }

        private void ValidateSumInRectangleCommand(string[] args, Sheet currentSheet)
        {
            this.ValidateArgsShouldHaveCount(args, 6);

            var values = this.ValidateAllArgsShouldBeNumeric(args);
            var x1 = values[0];
            var y1 = values[1];
            var x2 = values[2];
            var y2 = values[3];
            var x3 = values[4];
            var y3 = values[5];

            this.ValidateSheetShouldExist(currentSheet);
            this.ValidateCellIndexShouldBeInBounds(currentSheet, x1, y1);
            this.ValidateCellIndexShouldBeInBounds(currentSheet, x2, y2);
            this.ValidateCellIndexShouldBeInBounds(currentSheet, x3, y3);
        }

        private void ValidateArgsShouldHaveCount(string[] args, int expectedCount)
        {
            if (args.Length != expectedCount)
            {
                throw new ValidationException($"Arguments count should be equal {expectedCount}.");
            }
        }

        private int[] ValidateAllArgsShouldBeNumeric(string[] args)
        {
            var results = new List<int>(args.Length);

            foreach (var arg in args)
            {
                if (int.TryParse(arg, out int value))
                {
                    results.Add(value);
                }
                else
                {
                    throw new ValidationException($"Argument {arg} should be numeric.");
                }
            }

            return results.ToArray();
        }

        private void ValidateValueShouldBePositive(int value)
        {
            if (value <= 0)
            {
                throw new ValidationException($"Argument {value} should be positive.");
            }
        }

        private void ValidateSheetShouldExist(Sheet sheet)
        {
            if (sheet == null)
            {
                throw new ValidationException($"Sheet should exist for the command.");
            }
        }

        private void ValidateCellIndexShouldBeInBounds(Sheet sheet, int x, int y)
        {
            if (x < 0 || x >= sheet.Width)
            {
                throw new ValidationException($"Invalid index x={x} for sheet [{sheet.Width}, {sheet.Height}].");
            }

            if (y < 0 || y >= sheet.Height)
            {
                throw new ValidationException($"Invalid index y={y} for sheet [{sheet.Width}, {sheet.Height}].");
            }
        }
    }
}
