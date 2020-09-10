using HelperLibrary.Contracts;
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

        public (int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, string v9) TestManyTuple()
        {
            return (1, 2, 3, 4, 5, 6, 7, 8, "9");
        }

        public Task<(string v1, int v2)> TestTuple()
        {
            (string v1, int v2) = ("value1", 42);
            return Task.FromResult((v1, v2));
        }
    }
}
