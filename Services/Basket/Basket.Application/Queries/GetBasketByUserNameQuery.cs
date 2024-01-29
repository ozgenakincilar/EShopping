using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketByUserNameQuery:IRequest<ShoppingCardResponse>
    {
        public string UserName { get; set; }

        public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
