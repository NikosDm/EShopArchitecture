using System.Text.Json;
using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data
{
    // CachedBasketRepository follows the Decorator pattern here. 
    // That means that CachedBasketRepository works as a Decorator, meaning a class that implements 
    // the same class and will provide extensive implementation. 
    public class CachedBasketRepository
        (IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrWhiteSpace(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            
            var basket = await basketRepository.GetBasket(userName, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
    }
}