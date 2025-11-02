var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.lrembecki_host>("lrembecki-host");

await builder.Build().RunAsync();
