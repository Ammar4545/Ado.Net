
using Microsoft.Extensions.Configuration;

namespace Connection
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine(configuration.GetConnectionString("default")); 
        }
    }
}
