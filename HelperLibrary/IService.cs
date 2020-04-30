using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public interface IService
    {
        Task<string> Hello(string name);
    }
}
