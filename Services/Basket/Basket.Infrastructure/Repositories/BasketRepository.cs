using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCard> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if(string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCard>(basket);
        }

        public async Task<ShoppingCard> UpdateBasket(ShoppingCard shoppingCard)
        {
            await _redisCache.SetStringAsync(shoppingCard.UserName,JsonConvert.SerializeObject(shoppingCard));
            return await GetBasket(shoppingCard.UserName);
        }
    }
}
