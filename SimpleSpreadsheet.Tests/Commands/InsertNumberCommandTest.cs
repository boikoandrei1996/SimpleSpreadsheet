using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Tests.Commands
{
    public class InsertNumberCommandTest
    {
        [Test]
        public void Execute_ShouldInsertValue()
        {
            // Arrange
            var sheet = Sheet.Create(3, 3);
            var command = new InsertNumberCommand(sheet, 1, 1, -1);

            // Act
            var newSheet = command.Execute();

            // Assert
            newSheet.Should().NotBeNull();
            newSheet.GetValue(1, 1).Should().Be(-1);
        }
    }
}
