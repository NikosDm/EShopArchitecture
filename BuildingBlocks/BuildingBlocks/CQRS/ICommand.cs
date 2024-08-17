using MediatR;

namespace BuildingBlocks.CQRS
{
    // Unit is a representing weight type for the mediator.
    //So that's why we can define an empty or no generic type of the I command.
    public interface ICommand : ICommand<Unit> { }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        
    }
}