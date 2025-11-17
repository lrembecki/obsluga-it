var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.lrembecki_host>("lrembecki-host");

builder.AddAzureFunctionsProject<Projects.lrembecki_functions_trotamundos>("lrembecki-functions-trotamundos");

await builder.Build().RunAsync();
