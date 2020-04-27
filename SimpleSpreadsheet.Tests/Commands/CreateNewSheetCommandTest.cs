using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;

namespace SimpleSpreadsheet.Tests.Commands
{
    public class CreateNewSheetCommandTest
    {
        [Test]
        public void Execute_ShouldReturnNewSheet()
        {
            // Arrange
            var command = new CreateNewSheetCommand(3, 3);

            // Act
            var sheet = command.Execute();

            // Assert
            sheet.Should().NotBeNull();
            sheet.Width.Should().Be(3);
            sheet.Height.Should().Be(3);
        }
    }
}
