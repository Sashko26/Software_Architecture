using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresentationLayer.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Main_ValidName_ReturnsGreeting()
        {
            // Arrange
            string expectedGreeting = "Hello, John! Welcome to our application!";
            string input = "John";
            string actualGreeting;
            
            UserService userService = new UserService();
            
            // Act
            using (var consoleOutput = new ConsoleOutput())
            {
                consoleOutput.Enter(input);
                Program.Main(null);
                actualGreeting = consoleOutput.GetOutput();
            }
            
            // Assert
            Assert.AreEqual(expectedGreeting, actualGreeting);
        }
    }
    
    // Helper class to capture console input and output
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;
        
        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }
        
        public void Enter(string input)
        {
            Console.SetIn(new StringReader(input));
        }
        
        public string GetOutput()
        {
            return stringWriter.ToString().Trim();
        }
        
        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }
}
