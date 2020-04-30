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
    }
}
