using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CreateShoppingCardCommand : IRequest<ShoppingCardResponse>
    {
        public string UserName { get; set; }
        public List<ShoppingCardItem> Items { get; set; }

        public CreateShoppingCardCommand(List<ShoppingCardItem> items, string userName)
        {
            Items = items;
            UserName = userName;
        }
    }
}
