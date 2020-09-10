using System;

namespace HttpRPC.RPC
{
    public class ProxyException : Exception
    {
        public ProxyException(string message) : base(message) { }
    }
}
