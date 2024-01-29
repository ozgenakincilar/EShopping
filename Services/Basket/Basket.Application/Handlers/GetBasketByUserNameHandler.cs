using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler:IRequestHandler<GetBasketByUserNameQuery,ShoppingCardResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ShoppingCardResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCard= await _basketRepository.GetBasket(request.UserName);
            var shoppingCardResponse=BasketMapper.Mapper.Map<ShoppingCardResponse>(shoppingCard);
            return shoppingCardResponse;
        }
    }
}
