using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Reflection;

namespace HttpRPC.RPC.Client
{
    // Feature provider that lets the ProxyController to be loaded like any other "native" controller for a WebApi when configured in the Startup file
    public class ClientControllerFeatureProvider<T> : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var controllerType = typeof(ClientController<T>).GetTypeInfo();
            feature.Controllers.Add(controllerType);
        }
    }
}
