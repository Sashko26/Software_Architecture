using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterSlavePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Master master = new Master();

            // Create and add slave tasks to the master
            master.AddSlaveTask(new SlaveTask("Task 1"));
            master.AddSlaveTask(new SlaveTask("Task 2"));
            master.AddSlaveTask(new SlaveTask("Task 3"));

            // Start the master to execute the slave tasks
            master.Start();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class Master
    {
        private List<SlaveTask> slaveTasks = new List<SlaveTask>();

        public void AddSlaveTask(SlaveTask task)
        {
            slaveTasks.Add(task);
        }

        public void Start()
        {
            // Execute slave tasks asynchronously
            foreach (var task in slaveTasks)
            {
                Task.Run(() => task.Execute());
            }

            // Wait for all slave tasks to complete
            Task.WaitAll(slaveTasks.Select(t => t.Task).ToArray());

            // Process the results of slave tasks
            foreach (var task in slaveTasks)
            {
                Console.WriteLine("Slave task '{0}' result: {1}", task.Name, task.Result);
            }
        }
    }

    class SlaveTask
    {
        public string Name { get; private set; }
        public Task<string> Task { get; private set; }
        public string Result { get { return Task.Result; } }

        public SlaveTask(string name)
        {
            Name = name;
            Task = new Task<string>(() => ExecuteTask());
        }

        private string ExecuteTask()
        {
            // Simulate some processing time
            Task.Delay(1000).Wait();

            return "Result of " + Name;
        }

        public void Execute()
        {
            Task.Start();
        }
    }
}
