using Basket.Api.Data;
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);
    
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasket(command.Username, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}