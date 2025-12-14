var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.lrembecki_host>("lrembecki-host");
builder.AddAzureFunctionsProject<Projects.lrembecki_function_app>("lrembecki-function-app");

await builder.Build().RunAsync();
