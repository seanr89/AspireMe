using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Host_Api>("APIService");

builder.Build().Run();
