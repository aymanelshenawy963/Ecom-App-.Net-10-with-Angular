using Ecom.Core.Entites;
using Ecom.Core.interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Ecom.infrastructure.Repositriers;


public class CustomerBasketRepository : ICustomerBasketRepository
{
    private readonly IDatabase _database;

    public CustomerBasketRepository(IConnectionMultiplexer redis)
    {
       _database = redis.GetDatabase();
    }
    public async Task<CustomerBasket> GetBasketAsync(string id)
    {
        var result = await _database.StringGetAsync(id);

        if (result.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<CustomerBasket>(result.ToString());
    }
    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var _basket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(3));

        if (_basket)
        {
            return await GetBasketAsync(basket.Id);
        } 
        return null;
    }

    public Task<bool> DeleteBasketAsync(string id)
    {
        return _database.KeyDeleteAsync(id);
    }
}
