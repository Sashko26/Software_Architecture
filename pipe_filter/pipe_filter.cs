using System;
using System.Collections.Generic;
using System.Linq;

namespace PipeFilterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of input data
            List<int> inputData = Enumerable.Range(1, 10).ToList();

            // Create the filters
            Filter evenFilter = new EvenNumberFilter();
            Filter squareFilter = new SquareNumberFilter();

            // Create the pipe and connect the filters
            Pipe pipe = new Pipe();
            pipe.ConnectFilter(evenFilter);
            pipe.ConnectFilter(squareFilter);

            // Process the input data through the pipe
            List<int> outputData = pipe.Process(inputData);

            // Print the output data
            Console.WriteLine("Output data:");
            foreach (int number in outputData)
            {
                Console.WriteLine(number);
            }
        }
    }

    // Filter interface
    interface Filter
    {
        List<int> Process(List<int> input);
    }

    // EvenNumberFilter: Filter that keeps only the even numbers
    class EvenNumberFilter : Filter
    {
        public List<int> Process(List<int> input)
        {
            return input.Where(n => n % 2 == 0).ToList();
        }
    }

    // SquareNumberFilter: Filter that squares each number
    class SquareNumberFilter : Filter
    {
        public List<int> Process(List<int> input)
        {
            return input.Select(n => n * n).ToList();
        }
    }

    // Pipe class: Connects filters and processes the input data through them
    class Pipe
    {
        private List<Filter> filters = new List<Filter>();

        public void ConnectFilter(Filter filter)
        {
            filters.Add(filter);
        }

        public List<int> Process(List<int> input)
        {
            List<int> output = input;
            foreach (Filter filter in filters)
            {
                output = filter.Process(output);
            }
            return output;
        }
    }
}
