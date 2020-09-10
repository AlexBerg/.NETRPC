using HelperLibrary;
using System.Threading.Tasks;

namespace ServiceExample
{
    public class Service : IService
    {
        public Task<string> Hello(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult("Hello!");

            return Task.FromResult($"Hello {name}");
        }

        public bool IsTrue(bool value)
        {
            return value == true;
        }

        public Task<(string v1, int v2)> TestTuple()
        {
            (string v1, int v2) = ("value1", 42);
            return Task.FromResult((v1, v2));
        }
    }
}
