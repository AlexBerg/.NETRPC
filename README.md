# HttpRPC

This RPC solution works by generating classes during runtime that implements each method in a interface. For each method a method body is generated that puts all parameters into a dictionary and gets the uri to correct service from a ServiceFinder class. In the end a method is called in a separate class that sends a Http Post request to the requested service and handles the response.

## Json Serializer

This implementation utilizes the serializer provided in System.Text.Json instead of the Newtonsoft Json Serializer to keep the dependencies to third party packages to a minimum. However, I would recommend changing to the Newtonsoft package.

## Note regarding tuples

I am not sure why, but the normal ValueTuples do not behave as expected when used in the contracts for the RPC. That is why I implemented an approximation of them myself.

When playing around with the implementation I noticed that if I changed my tuple implemenation from a class to a struct it displays the same unexptected behaviour as the ValueTuple, which I believe to be struct as well. This leads me to, obviously, assume the problem is in how the tuples behave when implemented as structs.

# NOTE!

This solution requires the addition of the AddNewtonSoftJson to the IServiceCollection(see example service) in the Startup.
This adds a dependency on Microsoft.AspNetCore.Mvc.NewtonsoftJson.
