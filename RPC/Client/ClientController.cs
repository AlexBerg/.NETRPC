using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpRPC.RPC.Client
{
    [Route("client")]
    public class ClientController<T> : ControllerBase
    {
        private T Service;

        public ClientController(T service)
        {
            Service = service;
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Execute([FromBody]Dictionary<string, object> input)
        {
            try
            {
                // Get the correct method implementation in the service
                var type = Service.GetType();
                var methodName = (string)input["methodName"];
                input.Remove("methodName");
                var methods = type.GetMethods();
                MethodInfo method = null;
                object[] arguments = new object[] { };
                if (input != null && input.Count() != 0)
                {
                    method = methods.FirstOrDefault(m => m.Name == methodName && input.All(i => m.GetParameters().Any(a => a.Name == i.Key)));

                    var parameters = method.GetParameters();
                    // Get and convert all the inputs to match the input parameters to the method
                    arguments = GenerateArguments(parameters, input);
                }
                else
                    method = methods.FirstOrDefault(m => m.Name == methodName && m.GetParameters().Count() == 0);

                // Call the method and convert result to Task
                var task = (Task)method.Invoke(Service, arguments);

                await task;
                // Get the result from the method
                var resultProperty = task.GetType().GetProperty("Result");
                var result = resultProperty?.GetValue(task);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        // Creates a list for the parameters add any needed default parameter and converts all the provided inputs to the correct type
        protected object[] GenerateArguments(ParameterInfo[] parameters, Dictionary<string, object> inputs)
        {
            var arguments = new List<object>();
            foreach (var p in parameters)
            {
                if (!inputs.ContainsKey(p.Name))
                {
                    if (p.HasDefaultValue)
                    {
                        arguments.Add(p.DefaultValue);
                    }
                }
                else
                {
                    var value = inputs[p.Name];
                    if (value == null)
                    {
                        if (p.HasDefaultValue)
                            arguments.Add(p.DefaultValue);
                        else
                            arguments.Add(value);
                    }
                    else
                    {
                        var paramType = p.ParameterType;
                        if (value.GetType() != paramType)
                        {
                            var stringValue = JsonSerializer.Serialize(value);
                            value = JsonSerializer.Deserialize(stringValue, paramType);
                        }
                        arguments.Add(value);
                    }
                }
            }

            return arguments.ToArray();
        }
    }
}
