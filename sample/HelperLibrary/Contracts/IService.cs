using System.Threading.Tasks;

namespace HelperLibrary.Contracts
{
    public interface IService
    {
        Task<string> Hello(string name);
        bool IsTrue(bool value);
        Task<(string v1, int v2)> TestTuple();
        (int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, string v9) TestManyTuple();
    }
}
