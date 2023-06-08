// Layer 1: Presentation Layer
// This layer is responsible for handling user interface and interaction

using System;

namespace PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            
            // Calling the business logic from the service layer
            string greeting = userService.GetGreeting(name);
            
            Console.WriteLine(greeting);
        }
    }
}

// Layer 2: Business Layer
// This layer contains the business logic and rules

namespace BusinessLayer
{
    public class UserService
    {
        public string GetGreeting(string name)
        {
            return "Hello, " + name + "! Welcome to our application!";
        }
    }
}
