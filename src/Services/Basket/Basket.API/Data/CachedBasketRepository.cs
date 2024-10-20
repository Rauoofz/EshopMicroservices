
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasekt = await cache.GetStringAsync(username, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasekt))
            return JsonConvert.DeserializeObject<ShoppingCart>(cachedBasekt)!;

        var basket = await repository.GetBasket(username, cancellationToken);

        await cache.SetStringAsync(username, JsonConvert.SerializeObject(basket), cancellationToken);

        return basket;
    }
    public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        var basket = await repository.CreateBasket(cart, cancellationToken);

        await cache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket), cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        var isSuccess = await repository.DeleteBasket(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);

        return isSuccess;
    }
}
