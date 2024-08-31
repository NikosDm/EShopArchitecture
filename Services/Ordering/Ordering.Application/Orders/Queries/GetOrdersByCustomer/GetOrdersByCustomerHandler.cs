using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            // get orders by customer using dbContext
            // return result

            var orders = await dbContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            var orderItems = await dbContext.OrderItems
                .Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId))
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDTOList(orderItems));        
        }
    }
}