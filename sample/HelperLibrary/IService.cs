using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public interface IService
    {
        Task<string> Hello(string name);
        bool IsTrue(bool value);
        Task<(string v1, int v2)> TestTuple();
    }
}
