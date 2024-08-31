using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            // get orders by name using dbContext
            // return result

            var orders = await dbContext.Orders
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);                

            var orderItems = await dbContext.OrderItems
                .Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId))
                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDTOList(orderItems));
        }    
    }
}