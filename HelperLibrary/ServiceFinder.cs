using HttpRPC.RPC;
using System;

namespace HelperLibrary
{
    public class ServiceFinder : IServiceFinder
    {
        public string GetServiceUri(Type type)
        {
            if (type == typeof(IService))
                return "http://localhost:8025";
            else
                throw new Exception($"{type.Name} is not a valid contract");
        }
    }
}
