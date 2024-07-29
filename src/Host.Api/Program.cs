using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// builder.AddNpgsqlDataSource("Todos");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddNpgsqlDbContext<AppDbContext>("postgresdb");
builder.Services.AddScoped<CustomerService>();

var app = builder.Build().MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGet("/", () =>{
    app.Logger.LogInformation("Returning Hello!");
    return Results.Text("Hello World!");
});

app.MapGet("/customers", async (AppDbContext context) => Results.Ok(await context.Customers.ToListAsync())).WithOpenApi();

app.MapGet("/customers/{id}", async (Guid id, AppDbContext context) =>
{
    var cust = await context.Customers.FindAsync(id);
    return cust != null ? Results.Ok(cust) : Results.NotFound();
});

app.MapPost("/customers", async (AppDbContext context, Customer customer) =>
{
    context.Customers.Add(customer);
    await context.SaveChangesAsync();
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
