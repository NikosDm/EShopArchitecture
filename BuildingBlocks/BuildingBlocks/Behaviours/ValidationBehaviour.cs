using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviours
{
    // By using MediatR pipeline behavior we can add additional processing steps such as validation in our case. 
    // The alternative would be to add repetivice code for validation for all of our commands which is also error prone. 
    // In that way, we have cleaner and optimized code. 
    public class ValidationBehaviour<TRequest, TResponse> 
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            // That means complete all validation operations where this comes from validators that we have injected
            var validationResults = 
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0) throw new ValidationException(failures);

            return await next();
        }
    }
}