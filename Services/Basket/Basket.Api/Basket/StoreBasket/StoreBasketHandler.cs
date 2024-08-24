using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using FluentValidation;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreBasketHandler
        (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            // TODO Communicate with GRPC client
            await DeductDiscount(discountProtoServiceClient, command.Cart, cancellationToken);

            await basketRepository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.Username);
        }

        private static async Task DeductDiscount(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient, ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProtoServiceClient
                    .GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);

                item.Price -= coupon.Amount;
            }
        }
    }
}