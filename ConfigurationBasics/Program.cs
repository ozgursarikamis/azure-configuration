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

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables("ConfigurationBasics.");

            var config = builder.Build();

            Console.WriteLine($"{config["HelloMessage"]}");
            // in the appsettings.json file, this part is missing:
            Console.WriteLine($"{config["GoodbyeMessage"]}");
        }
    }
}
