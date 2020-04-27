using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleSpreadsheet.Commands;
using SimpleSpreadsheet.Exceptions;

namespace SimpleSpreadsheet.Tests.Commands
{
    public class QuitCommandTest
    {
        [Test]
        public void Execute_ShouldThrowQuitProgramException()
        {
            // Arrange
            var command = new QuitCommand();

            // Act
            Action action = () => command.Execute();

            // Assert
            action.Should().ThrowExactly<QuitProgramException>();
        }
    }
}
