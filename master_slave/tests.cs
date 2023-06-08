using System;
using Xunit;

namespace MasterSlavePattern.Tests
{
    public class MasterTests
    {
        [Fact]
        public void Start_ExecutesAllSlaveTasks()
        {
            // Arrange
            Master master = new Master();
            var slaveTask1 = new SlaveTask("Task 1");
            var slaveTask2 = new SlaveTask("Task 2");

            master.AddSlaveTask(slaveTask1);
            master.AddSlaveTask(slaveTask2);

            // Act
            master.Start();

            // Assert
            Assert.True(slaveTask1.Task.IsCompleted);
            Assert.True(slaveTask2.Task.IsCompleted);
        }

        [Fact]
        public void Start_ProcessesSlaveTaskResults()
        {
            // Arrange
            Master master = new Master();
            var slaveTask1 = new SlaveTask("Task 1");
            var slaveTask2 = new SlaveTask("Task 2");

            master.AddSlaveTask(slaveTask1);
            master.AddSlaveTask(slaveTask2);

            // Act
            master.Start();

            // Assert
            Assert.Equal("Result of Task 1", slaveTask1.Result);
            Assert.Equal("Result of Task 2", slaveTask2.Result);
        }
    }

    public class SlaveTaskTests
    {
        [Fact]
        public void Execute_SimulatesProcessingTime()
        {
            // Arrange
            var slaveTask = new SlaveTask("Task");

            // Act
            slaveTask.Execute();

            // Assert
            Assert.True(slaveTask.Task.IsCompleted);
        }
    }
}
