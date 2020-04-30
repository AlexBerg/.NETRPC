using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        public async Task<object> Execute(string uri, string methodName, string typeName, Dictionary<string, object> input)
        {
            try
            {
                // Create a http request to the proxy controller injected into each service
                var type = Type.GetType(typeName);
                input.Add("methodName", methodName);
                var response = await SendRequest($"{uri}/client", input);
                if(string.IsNullOrWhiteSpace(response))
                {
                    if (type.IsValueType)
                        return Activator.CreateInstance(type);
                    else
                        return null;
                }

                var result = JsonSerializer.Deserialize(response, type, new JsonSerializerOptions { IgnoreNullValues = true });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<string> SendRequest(string uri, Dictionary<string, object> input)
        {
            var stringInput = JsonSerializer.Serialize(input, new JsonSerializerOptions { IgnoreNullValues = true });
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
