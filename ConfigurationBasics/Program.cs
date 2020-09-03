using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ConfigurationBasics
{
    internal class Program
    {
        private static void Main()
        {
            var settings = new Dictionary<string, string>
            {
                ["HelloMessage"] = "Hello, World",
                ["GoodbyeMessage"] = "Goodbye, World"
            };

            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(settings);

            var config = builder.Build();

            Console.WriteLine($"{config["HelloMessage"]}");
            Console.WriteLine($"{config["GoodbyeMessage"]}");
        }
    }
}
