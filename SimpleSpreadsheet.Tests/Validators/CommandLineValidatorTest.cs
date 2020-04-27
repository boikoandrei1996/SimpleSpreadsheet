using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Models;
using SimpleSpreadsheet.Validators;

namespace SimpleSpreadsheet.Tests.Validators
{
    public class CommandLineValidatorTest
    {
        private Sheet _sheet;

        private ICommandValidator _sut;

        private static readonly IEnumerable<string[]> InvalidArgsForCreateNewSheetCommand = new[]
        {
            new string[] { "2" }, // args count less than 2
            new string[] { "test", "2" }, // args value not numeric
            new string[] { "0", "2" }, // width value not positive
            new string[] { "-1", "2" }, // width value not positive
            new string[] { "2", "0" }, // height value not positive
            new string[] { "2", "-1" } // height value not positive
        };

        private static readonly IEnumerable<string[]> InvalidArgsForInsertNumberCommand = new[]
        {
            new string[] { "1", "2" }, // args count less than 3
            new string[] { "1", "2", "test" }, // args value not numeric
            new string[] { "10", "12", "1" }, // cell not in sheet bould 
            new string[] { "-1", "2", "1" }, // cell x not in sheet bould
            new string[] { "2", "-1", "1" } // cell y not in sheet bould
        };

        private static readonly IEnumerable<string[]> InvalidArgsForSumInRectangleCommand = new[]
        {
            new string[] { "1", "1", "2", "2" }, // args count less than 6
            new string[] { "test", "1", "2", "2", "2", "3" }, // args value not numeric
            new string[] { "0", "2", "1", "2", "12", "12" }, // cell not in sheet bould
            new string[] { "0", "2", "1", "2", "-1", "2" }, // cell x not in sheet bould
            new string[] { "0", "2", "1", "2", "2", "-1" } // cell y not in sheet bould
        };

        [SetUp]
        public void Setup()
        {
            _sheet = Sheet.Create(4, 4);
            _sut = new CommandLineValidator();
        }

        [TearDown]
        public void TearDown()
        {
            _sheet = null;
            _sut = null;
        }

        [Test]
        public void Validate_WithNullArgs_ShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action action = () => _sut.Validate(CommandType.CreateNewSheet, null, null);

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void Validate_CreateNewSheetCommand_WithValidArgs_ShouldNotThrowException()
        {
            // Arrange
            string[] args = new[] { "2", "3" };

            // Act
            Action action = () => _sut.Validate(CommandType.CreateNewSheet, args, null);

            // Assert
            action.Should().NotThrow();
        }

        [Test]
        [TestCaseSource(nameof(InvalidArgsForCreateNewSheetCommand))]
        public void Validate_CreateNewSheetCommand_WithInvalidArgs_ShouldThrowValidationException(string[] args)
        {
            // Arrange

            // Act
            Action action = () => _sut.Validate(CommandType.CreateNewSheet, args, null);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Validate_InsertNumberCommand_WithValidArgs_ShouldNotThrowException()
        {
            // Arrange
            string[] args = new[] { "2", "3", "-1" };

            // Act
            Action action = () => _sut.Validate(CommandType.InsertNumber, args, _sheet);

            // Assert
            action.Should().NotThrow();
        }

        [Test]
        [TestCaseSource(nameof(InvalidArgsForInsertNumberCommand))]
        public void Validate_InsertNumberCommand_WithInvalidArgs_ShouldThrowValidationException(string[] args)
        {
            // Arrange

            // Act
            Action action = () => _sut.Validate(CommandType.InsertNumber, args, _sheet);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Validate_InsertNumberCommand_WithNullCurrentSheet_ShouldThrowValidationException()
        {
            // Arrange
            string[] args = new[] { "2", "3", "-1" };

            // Act
            Action action = () => _sut.Validate(CommandType.InsertNumber, args, null);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Validate_SumInRectangleCommand_WithValidArgs_ShouldNotThrowException()
        {
            // Arrange
            string[] args = new[] { "1", "3", "2", "3", "3", "3" };

            // Act
            Action action = () => _sut.Validate(CommandType.SumInRectangle, args, _sheet);

            // Assert
            action.Should().NotThrow();
        }

        [Test]
        [TestCaseSource(nameof(InvalidArgsForSumInRectangleCommand))]
        public void Validate_SumInRectangleCommand_WithInvalidArgs_ShouldThrowValidationException(string[] args)
        {
            // Arrange

            // Act
            Action action = () => _sut.Validate(CommandType.SumInRectangle, args, _sheet);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Validate_SumInRectangleCommand_WithNullCurrentSheet_ShouldThrowValidationException()
        {
            // Arrange
            string[] args = new[] { "1", "3", "2", "3", "3", "3" };

            // Act
            Action action = () => _sut.Validate(CommandType.SumInRectangle, args, null);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }
    }
}
