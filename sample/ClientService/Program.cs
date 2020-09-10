using HelperLibrary;
using HelperLibrary.Contracts;
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
            await TestTuple();
            TestBool();
        }

        private static async Task SendRequestHello()
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

        private static void TestBool()
        {
            var isTrue = _service.IsTrue(true);
            if (isTrue)
                Console.WriteLine("It was true");
            else
                Console.WriteLine("It was false?!");

            Console.WriteLine("Enter any key to quit");
            Console.ReadLine();
        }

        private static async Task TestTuple()
        {
            var tuple = await _service.TestTuple();
            Console.WriteLine($"v1: {tuple.v1}, v2: {tuple.v2}");
            var tuple2 = _service.TestManyTuple();
            Console.WriteLine("Enter any key to quit");
            Console.ReadLine();
        }
    }
}
