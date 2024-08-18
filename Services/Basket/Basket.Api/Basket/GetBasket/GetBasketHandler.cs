using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;

namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketQuery(string Username) : IQuery<GetBaskerResult>;
    public record GetBaskerResult(ShoppingCart Cart);

    public class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBaskerResult>
    {
        public async Task<GetBaskerResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.Username, cancellationToken);

            return new GetBaskerResult(basket);
        }
    }
}