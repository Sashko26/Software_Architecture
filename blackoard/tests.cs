using System;
using Xunit;

namespace BlackboardPattern.Tests
{
    public class BlackboardTests
    {
        [Fact]
        public void GetData_ExistingKey_ReturnsData()
        {
            // Arrange
            var blackboard = new Blackboard();
            var key = "key";
            var expectedData = "data";
            blackboard.SetData(key, expectedData);

            // Act
            var actualData = blackboard.GetData(key);

            // Assert
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetData_NonExistingKey_ReturnsNull()
        {
            // Arrange
            var blackboard = new Blackboard();
            var key = "key";

            // Act
            var actualData = blackboard.GetData(key);

            // Assert
            Assert.Null(actualData);
        }

        [Fact]
        public void SetData_KeyValueStored()
        {
            // Arrange
            var blackboard = new Blackboard();
            var key = "key";
            var data = "data";

            // Act
            blackboard.SetData(key, data);

            // Assert
            var storedData = blackboard.GetData(key);
            Assert.Equal(data, storedData);
        }

        [Fact]
        public void ComponentA_Update_ProcessesDataAndStoresResultOnBlackboard()
        {
            // Arrange
            var blackboard = new Blackboard();
            var componentA = new ComponentA(blackboard);
            var data = "Data A";
            blackboard.SetData("dataA", data);
            var expectedProcessedData = $"Processed data A: {data}";

            // Act
            componentA.Update();

            // Assert
            var resultA = blackboard.GetData("resultA");
            Assert.Equal(expectedProcessedData, resultA);
        }

        [Fact]
        public void ComponentB_Update_ProcessesDataAndStoresResultOnBlackboard()
        {
            // Arrange
            var blackboard = new Blackboard();
            var componentB = new ComponentB(blackboard);
            var data = "Data B";
            blackboard.SetData("dataB", data);
            var expectedProcessedData = $"Processed data B: {data}";

            // Act
            componentB.Update();

            // Assert
            var resultB = blackboard.GetData("resultB");
            Assert.Equal(expectedProcessedData, resultB);
        }
    }
}
