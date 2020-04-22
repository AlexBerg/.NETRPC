# HttpRPC

This RPC solution works by generating classes during runtime that implements each method in a interface. For each method a method body is generated that puts all parameters into a dictionary and gets the uri to correct service from a ServiceFinder class. In the end a method is called in a separate class that sends a Http Post request to the requested service and handles the response.

## Json Serializer

This implementation utilizes the serializer provided in System.Text.Json instead of the Newtonsoft Json Serializer to keep the dependencies to third party packages to a minimum.
