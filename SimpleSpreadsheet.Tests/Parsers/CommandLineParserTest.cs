using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Exceptions;
using SimpleSpreadsheet.Parsers;

namespace SimpleSpreadsheet.Tests.Parsers
{
    public class CommandLineParserTest
    {
        private static readonly char LineSeparator = ' ';

        private ICommandParser _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CommandLineParser();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null;
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Parse_WithNullOrEmptyLine_ShouldThrowValidationException(string line)
        {
            // Arrange

            // Act
            Func<(CommandType type, string[] args)> action = () => _sut.Parse(line, LineSeparator);

            // Assert
            action.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void Parse_WithCreateNewSheetType_ShouldReturnCorrectCommandType()
        {
            // Arrange
            string line = "c 20 4";

            // Act
            (CommandType type, string[] args) = _sut.Parse(line, LineSeparator);

            // Assert
            type.Should().BeEquivalentTo(CommandType.CreateNewSheet);
            args.Should().HaveCount(2);
        }

        [Test]
        public void Parse_WithInsertNumberType_ShouldReturnCorrectCommandType()
        {
            // Arrange
            string line = "n 1 2 2";

            // Act
            (CommandType type, string[] args) = _sut.Parse(line, LineSeparator);

            // Assert
            type.Should().BeEquivalentTo(CommandType.InsertNumber);
            args.Should().HaveCount(3);
        }

        [Test]
        public void Parse_WithSumInRectangleType_ShouldReturnCorrectCommandType()
        {
            // Arrange
            string line = "s 1 2 1 3 1 4";

            // Act
            (CommandType type, string[] args) = _sut.Parse(line, LineSeparator);

            // Assert
            type.Should().BeEquivalentTo(CommandType.SumInRectangle);
            args.Should().HaveCount(6);
        }

        [Test]
        public void Parse_WithQuitType_ShouldReturnCorrectCommandType()
        {
            // Arrange
            string line = "q";

            // Act
            (CommandType type, string[] args) = _sut.Parse(line, LineSeparator);

            // Assert
            type.Should().BeEquivalentTo(CommandType.Quit);
            args.Should().HaveCount(0);
        }

        [Test]
        public void Parse_WithNotSupportedCommandType_ShouldThrowCommandTypeException()
        {
            // Arrange
            string line = "new 1 2";

            // Act
            Func<(CommandType type, string[] args)> action = () => _sut.Parse(line, LineSeparator);

            // Assert
            action.Should().ThrowExactly<CommandTypeException>();
        }
    }
}
