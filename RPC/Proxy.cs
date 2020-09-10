using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

        #region Value Type executions

        // All the following methods are for handling Value Type unboxing because the entire rest of the RPC call chain deals exclusively with the object type, meaning that all Value Types are boxed to a Reference Type.
        // Without this the end result from the RPC call becomes a random value of the result type. I am not 100% sure why this is, however, I believe it because when dealing with Value Types is takes the value straight from the stack,
        // while when you deal with Reference Types there is memory address on the stack(i.e a reference) and the actual value is place in heap memory, leading to implicit conversion actually converting the address, instead of the value stored at the address.
        // Explicit conversion on the other hand copies the value stored at the address to the value on the stack
        public int ExecuteInt32(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (int)result;
        }
        public uint ExecuteUInt32(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (uint)result;
        }
        public short ExecuteInt16(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (short)result;
        }
        public ushort ExecuteUInt16(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ushort)result;
        }
        public long ExecuteInt64(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (long)result;
        }
        public ulong ExecuteUInt64(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ulong)result;
        }
        public float ExecuteSingle(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (float)result;
        }
        public double ExecuteDouble(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (double)result;
        }
        public char ExecuteChar(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (char)result;
        }
        public bool ExecuteBoolean(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (bool)result;
        }
        public decimal ExecuteDecimal(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (decimal)result;
        }
        public DateTime ExecuteDateTime(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (DateTime)result;
        }
        public byte ExecuteByte(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (byte)result;
        }
        public sbyte ExecuteSByte(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (sbyte)result;
        }
        public ValueTuple ExecuteValueTuple(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple)result;
        }
        public ValueTuple<T1, T2> ExecuteValueTuple2<T1, T2>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2>)result;
        }
        public ValueTuple<T1, T2, T3> ExecuteValueTuple3<T1, T2, T3>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3>)result;
        }
        public ValueTuple<T1, T2, T3, T4> ExecuteValueTuple4<T1, T2, T3, T4>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4>)result;
        }
        public ValueTuple<T1, T2, T3, T4, T5> ExecuteValueTuple5<T1, T2, T3, T4, T5>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5>)result;
        }
        public ValueTuple<T1, T2, T3, T4, T5, T6> ExecuteValueTuple6<T1, T2, T3, T4, T5, T6>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5, T6>)result;
        }
        public ValueTuple<T1, T2, T3, T4, T5, T6, T7> ExecuteValueTuple7<T1, T2, T3, T4, T5, T6, T7>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = Execute(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)result;
        }

        public async Task<int> ExecuteAsyncInt32(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (int)result;
        }
        public async Task<uint> ExecuteAsyncUInt32(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (uint)result;
        }
        public async Task<short> ExecuteAsyncInt16(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (short)result;
        }
        public async Task<ushort> ExecuteAsyncUInt16(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ushort)result;
        }
        public async Task<long> ExecuteAsyncInt64(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (long)result;
        }
        public async Task<ulong> ExecuteAsyncUInt64(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ulong)result;
        }
        public async Task<float> ExecuteAsyncSingle(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (float)result;
        }
        public async Task<double> ExecuteAsyncDouble(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (double)result;
        }
        public async Task<char> ExecuteAsyncChar(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (char)result;
        }
        public async Task<bool> ExecuteAsyncBoolean(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (bool)result;
        }
        public async Task<decimal> ExecuteAsyncDecimal(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (decimal)result;
        }
        public async Task<DateTime> ExecuteAsyncDateTime(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (DateTime)result;
        }
        public async Task<byte> ExecuteAsyncByte(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (byte)result;
        }
        public async Task<sbyte> ExecuteAsyncSByte(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (sbyte)result;
        }
        public async Task<ValueTuple> ExecuteAsyncValueTuple(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple)result;
        }
        public async Task<ValueTuple<T1, T2>> ExecuteAsyncValueTuple2<T1, T2>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2>)result;
        }
        public async Task<ValueTuple<T1, T2, T3>> ExecuteAsyncValueTuple3<T1, T2, T3>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3>)result;
        }
        public async Task<ValueTuple<T1, T2, T3, T4>> ExecuteAsyncValueTuple4<T1, T2, T3, T4>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4>)result;
        }
        public async Task<ValueTuple<T1, T2, T3, T4, T5>> ExecuteAsyncValueTuple5<T1, T2, T3, T4, T5>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5>)result;
        }
        public async Task<ValueTuple<T1, T2, T3, T4, T5, T6>> ExecuteAsyncValueTuple6<T1, T2, T3, T4, T5, T6>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5, T6>)result;
        }
        public async Task<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> ExecuteAsyncValueTuple7<T1, T2, T3, T4, T5, T6, T7>(string uri, string methodName, string typeName, Dictionary<string, object> inputs)
        {
            var result = await ExecuteAsync(uri, methodName, typeName, inputs);
            return (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)result;
        }
        #endregion
    }
}
