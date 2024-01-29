using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCardCommandHandler:IRequestHandler<CreateShoppingCardCommand, ShoppingCardResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCardCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ShoppingCardResponse> Handle(CreateShoppingCardCommand request, CancellationToken cancellationToken)
        {
            //TODO: use discount service and apply coupons

            var shoppingCard = await _basketRepository.UpdateBasket(new ShoppingCard
            {
                UserName = request.UserName,
                Items= request.Items,
            });

            var shoppingCardResponse=BasketMapper.Mapper.Map<ShoppingCardResponse>(shoppingCard);
            return shoppingCardResponse;
        }
    }
}
