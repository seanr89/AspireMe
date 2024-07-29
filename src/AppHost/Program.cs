
var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL container is configured with an auto-generated password by default
// and supports setting the default database name via an environment variable & running *.sql/*.sh scripts in a bind mount.
// var todosDbName = "Todos";
// var todosDb = builder.AddPostgres("postgres")
//     // Set the name of the default database to auto-create on container startup.
//     .WithEnvironment("POSTGRES_DB", todosDbName)
//     // Mount the SQL scripts directory into the container so that the init scripts run.
//     //.WithBindMount("../DatabaseContainers.ApiService/data/postgres", "/docker-entrypoint-initdb.d")
//     // Add the default database to the application model so that it can be referenced by other resources.
//     .AddDatabase(todosDbName);


var postgres = builder.AddPostgres("postgres").AddDatabase("postgresdb");

builder.AddProject<Projects.Host_Api>("WebApp")
    .WithReference(postgres);

builder.Build().Run();
