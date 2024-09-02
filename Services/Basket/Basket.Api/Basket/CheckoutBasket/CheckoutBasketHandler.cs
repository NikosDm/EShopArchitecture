using Basket.Api.Data;
using Basket.Api.DTOs;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Carter;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO) 
        : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDTO).NotNull().WithMessage("BasketCheckoutDTO can't be null");
            RuleFor(x => x.BasketCheckoutDTO.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckoutBasketHandler
        (IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.Price;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckoutDTO.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}