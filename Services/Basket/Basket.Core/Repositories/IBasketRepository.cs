using Basket.Core.Entities;

namespace Basket.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCard> GetBasket(string userName);
        Task<ShoppingCard> UpdateBasket(ShoppingCard shoppingCard);
        Task DeleteBasket(string userName);
    }
}
