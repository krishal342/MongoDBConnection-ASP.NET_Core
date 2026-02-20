using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBConnection.Config;
using MongoDBConnection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("Database"));

// register MongoClient as a singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var databaseConfig = sp.GetRequiredService<IOptions<DatabaseConfig>>().Value;
    return new MongoClient(databaseConfig.ConnectionString);
});

builder.Services.AddScoped<StudentsService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
