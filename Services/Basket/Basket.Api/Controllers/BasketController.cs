using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{

    public class BasketController : ApiController
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCardResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCardResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCardResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCardResponse>> UpdateBasket([FromBody] CreateShoppingCardCommand createShoppingCardCommand)
        {
            var basket = await _mediator.Send(createShoppingCardCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType( (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCardResponse>> DeleteBasket(string userName)
        {
            var query = new DeleteBasketByUserNameCommand(userName);
             return Ok(await _mediator.Send(query));
        }

    }
}
