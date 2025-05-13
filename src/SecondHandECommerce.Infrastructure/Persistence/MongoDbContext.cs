using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace SecondHandECommerce.Infrastructure.Persistence;

public static class MongoDbContext
{
    public static void AddMongo(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["Mongo:ConnectionString"];
        var databaseName = config["Mongo:Database"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        services.AddSingleton<IMongoDatabase>(database);
    }
}