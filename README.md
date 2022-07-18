# .NETRPC

This RPC solution works by generating classes during runtime that implements each method in a interface. For each method a method body is generated that puts all parameters into a dictionary and gets the uri to correct service from a ServiceFinder class. In the end a method is called in a separate class that sends a Http Post request to the requested service and handles the response.

## Dependencies

This project has dependencies on following packages:
* Microsoft.AspNetCore.Mvc.Abstractions version: 2.2.0
* Microsoft.AspNetCore.Mvc.Core version: 2.2.5
* Newtonsoft.Json version:13.0.1

## Target Framework
The actual RPC project targets .NET Standard 2.1
