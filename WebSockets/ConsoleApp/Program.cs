using System;
using System.Threading.Tasks;
using WebService.Client;
using WebService.Common;

namespace ConsoleApp
{
    class Program
    {
        private static Connection _connection;
        private static StringMethodInvocationStrategy _strategy;
        private static String link = "localhost:65109";

        static void Main(string[] args)
        {
            StartConnectionAsync();

            _strategy.On("receiveMessage", (arguments) =>
            {
                Console.WriteLine($"{arguments[0]} said: {arguments[1]}");
            });

            Console.ReadLine();
             
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key.Equals(ConsoleKey.Escape))
            { 
                StopConnectionAsync();
            } 
        }

        private static async Task StartConnectionAsync()
        {
            _strategy = new StringMethodInvocationStrategy();
            _connection = new Connection(_strategy);
            await _connection.StartConnectionAsync("ws://" + link + "/chat");
        }

        private static async Task StopConnectionAsync()
        {
            await _connection.StopConnectionAsync();
        }

    }
}
