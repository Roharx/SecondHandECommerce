using MongoDB.Driver;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoDatabase _database;

    public OrderRepository(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task CreateAsync(Order order)
    {
        using var session = await _database.Client.StartSessionAsync();
        session.StartTransaction();

        try
        {
            var orders = _database.GetCollection<Order>("Orders");
            await orders.InsertOneAsync(session, order);

            // Optionally update listing availability here if needed
            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var orders = _database.GetCollection<Order>("Orders");
        return await orders.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Order>> GetByBuyerIdAsync(Guid buyerId)
    {
        var orders = _database.GetCollection<Order>("Orders");
        return await orders.Find(o => o.BuyerId == buyerId).ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var orders = _database.GetCollection<Order>("Orders");
        await orders.ReplaceOneAsync(o => o.Id == order.Id, order);
    }
}