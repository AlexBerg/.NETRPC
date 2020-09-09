using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;

namespace HttpRPC.RPC
{
    public class ClientGenerator
    {
        // This method is generates a class during runtime that implements a interface given in Type argument
        public static T GenerateClass<T>(IServiceFinder serviceFinder, HttpClient http, Type[] inheritedTypes = null)
        {
            try
            {
                var t = typeof(T);

                if (!t.IsInterface)
                    throw new ProxyException("Only interface types are allowed");

                var typeBuilder = CreateTypeBuilder(); // Creates a class type

                var methods = t.GetMethods().ToList();
                if(inheritedTypes != null)
                    foreach(Type type in inheritedTypes)
                    {
                        methods.AddRange(type.GetMethods());
                    }

                // Add a field to the class
                var field = CreateFiled(typeBuilder);
                // Add a constructor to the class
                CreateCotr(typeBuilder, field);

                var serviceUri = serviceFinder.GetServiceUri(typeof(T));

                foreach (var m in methods)
                {
                    // Create and add a method implementation for each method in the interface to the class
                    CreateMethod(m, typeBuilder, field, serviceUri);
                }

                typeBuilder.AddInterfaceImplementation(t); // Add interface implementation to the class

                return (T)Activator.CreateInstance(typeBuilder.CreateTypeInfo().AsType(), new Proxy(http)); // Create class type and instantiate it
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static FieldInfo CreateFiled(TypeBuilder typeBuilder)
        {
            // Add the private Proxy _proxy field to the class
            return typeBuilder.DefineField("_proxy", typeof(Proxy), FieldAttributes.Private);
        }

        private static void CreateCotr(TypeBuilder typeBuilder, FieldInfo proxyField)
        {
            // Create constructor for the class that that takes a input argument of type ServiceProxy
            var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public
                , CallingConventions.Standard
                , new[] { typeof(Proxy) });

            var il = ctorBuilder.GetILGenerator(); // Get Intermediate Language Generator for the constructor

            // Adds a body to the constructor that updates the value of the field with the input value
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, proxyField);
            il.Emit(OpCodes.Ret);
        }

        // Adds a method and method body to the class
        private static void CreateMethod(MethodInfo m, TypeBuilder tb, FieldInfo proxyField, string serviceUri)
        {
            var parameters = m.GetParameters();
            // Create the method definetion
            var mb = tb.DefineMethod(m.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, CallingConventions.Standard, m.ReturnType, parameters.Select(p => p.ParameterType).ToArray());

            var il = mb.GetILGenerator();
            // Creates a local variable of type Dictionary<string,object>
            il.DeclareLocal(typeof(Dictionary<string, object>));
            il.Emit(OpCodes.Newobj, typeof(Dictionary<string, object>).GetConstructor(new Type[0]));
            il.Emit(OpCodes.Stloc_0);

            var i = 0;
            // Adds each input parameter to the dictionary, Dictionary.Add(nameof(p), p)
            foreach (var p in parameters)
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldstr, p.Name);
                il.Emit(OpCodes.Ldarg, (short)(i + 1));

                if (p.ParameterType.GetTypeInfo().IsValueType)
                    il.Emit(OpCodes.Box, p.ParameterType);

                il.Emit(OpCodes.Callvirt, typeof(Dictionary<string, object>).GetMethod("Add"));

                i += 1;
            }

            Type tArgument = null;
            if (m.ReturnType.IsGenericType)
                tArgument = m.ReturnType.GenericTypeArguments[0];
            else
                tArgument = m.ReturnType;

            var isTask = m.ReturnType.Name.Contains("Task");

            var methodName = isTask ? "ExecuteAsync" : "Execute";
            methodName = GetValueTypeExecutionMethodName(methodName, tArgument);

            var proxyMethod = typeof(Proxy).GetMethod(methodName);
            // Adds a method call to the Execute method in ServiceProxy on the _proxy field with the service uri, method name and method return type name as inputs
            // and then returns from the method body
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, proxyField);
            il.Emit(OpCodes.Ldstr, serviceUri);
            il.Emit(OpCodes.Ldstr, m.Name);
            il.Emit(OpCodes.Ldstr, tArgument.AssemblyQualifiedName);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Callvirt, proxyMethod);
            il.Emit(OpCodes.Ret);
        }

        // Defines a dynamic class type
        private static TypeBuilder CreateTypeBuilder()
        {
            var typeSignature = "DynamicType";
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            var tb = moduleBuilder.DefineType(typeSignature, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout, null);

            return tb;
        }

        private static string GetValueTypeExecutionMethodName(string baseName, Type type)
        {
            var returnName = baseName;
            if (type.IsValueType)
                returnName += type.Name;

            return returnName.Replace("`", "");
        }
    }
}
