using System;

namespace HttpRPC.RPC
{
    public interface IServiceFinder
    {
        string GetServiceUri(Type type);
    }
}
