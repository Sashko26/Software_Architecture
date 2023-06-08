using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PipeFilterPattern.Tests
{
    [TestClass]
    public class PipeFilterTests
    {
        [TestMethod]
        public void EvenNumberFilter_Process_ReturnsOnlyEvenNumbers()
        {
            // Arrange
            List<int> input = Enumerable.Range(1, 10).ToList();
            Filter filter = new EvenNumberFilter();

            // Act
            List<int> output = filter.Process(input);

            // Assert
            Assert.AreEqual(5, output.Count);
            Assert.IsTrue(output.All(n => n % 2 == 0));
        }

        [TestMethod]
        public void SquareNumberFilter_Process_SquaresEachNumber()
        {
            // Arrange
            List<int> input = Enumerable.Range(1, 5).ToList();
            Filter filter = new SquareNumberFilter();

            // Act
            List<int> output = filter.Process(input);

            // Assert
            Assert.AreEqual(5, output.Count);
            CollectionAssert.AreEqual(new List<int> { 1, 4, 9, 16, 25 }, output);
        }

        [TestMethod]
        public void Pipe_Process_ChainsFiltersCorrectly()
        {
            // Arrange
            List<int> input = Enumerable.Range(1, 10).ToList();
            Filter evenFilter = new EvenNumberFilter();
            Filter squareFilter = new SquareNumberFilter();
            Pipe pipe = new Pipe();
            pipe.ConnectFilter(evenFilter);
            pipe.ConnectFilter(squareFilter);

            // Act
            List<int> output = pipe.Process(input);

            // Assert
            Assert.AreEqual(5, output.Count);
            Assert.IsTrue(output.All(n => n % 4 == 0));
        }
    }
}
