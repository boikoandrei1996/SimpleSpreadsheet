using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Models;

namespace SimpleSpreadsheet.Tests.Commands
{
    public class SumInRectangleCommandTest
    {
        [Test]
        public void Execute_ShouldGetSumOfValuesAndInsertTo()
        {
            // Arrange
            var sheet = Sheet.Create(3, 3);
            sheet.SetValue(1, 1, 1);
            sheet.SetValue(1, 2, 2);
            sheet.SetValue(2, 1, 3);
            var command = new SumInRectangleCommand(sheet, 1, 1, 2, 2, 0, 0);

            // Act
            var newSheet = command.Execute();

            // Assert
            newSheet.Should().NotBeNull();
            newSheet.GetValue(0, 0).Should().Be(6);
        }
    }
}
