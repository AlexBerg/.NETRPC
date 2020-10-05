using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRPC.RPC
{
    public class Proxy
    {
        protected HttpClient Http;

        public Proxy(HttpClient http)
        {
            Http = http;
        }

        public T ExecuteValueType<T>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = ExecuteAsync(uri, methodName, typeName, inputs).GetAwaiter().GetResult();
            return (T)result;
        }

        public async Task<T> ExecuteAsyncValueType<T>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (T)result;
        }

        public object Execute(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = ExecuteAsync(uri, methodName, typeName, inputs).GetAwaiter().GetResult();
            return result;
        }

        public async Task<object> ExecuteAsync(string uri, string methodName, string typeName, Dictionary<string, object> input)
        {
            try
            {
                // Create a http request to the proxy controller injected into each service
                var type = Type.GetType(typeName);
                input.Add("methodName", methodName);
                var response = await SendRequest($"{uri}/client", input);
                if (string.IsNullOrWhiteSpace(response))
                {
                    if (type.IsValueType)
                        return Activator.CreateInstance(type);
                    else
                        return null;
                }
                if (type == typeof(string))
                    return response;

                var result = JsonConvert.DeserializeObject(response, type, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<string> SendRequest(string uri, Dictionary<string, object> input)
        {
            var stringInput = JsonConvert.SerializeObject(input, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            var payload = new StringContent(stringInput, Encoding.UTF8, "application/json");

            var response = await Http.PostAsync(uri, payload);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"{response.StatusCode}: {errorMsg}");
            }
            if (response.Content == null)
                return "";

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
