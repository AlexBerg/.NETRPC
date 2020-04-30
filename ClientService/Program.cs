using HelperLibrary;
using HttpRPC.RPC;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientService
{
    class Program
    {
        private static IService _service;
        static async Task Main()
        {
            _service = ClientGenerator.GenerateClass<IService>(new ServiceFinder(), new HttpClient());
            await SendRequest();
        }

        private static async Task SendRequest()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            while (true)
            {
                var greeting = await _service.Hello(name);
                Console.WriteLine(greeting);
                Console.WriteLine("Enter another name or q to quit");
                name = Console.ReadLine();
                if (name == "q")
                    break;
            }
        }
    }
}
