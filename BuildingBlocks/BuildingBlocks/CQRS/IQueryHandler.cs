using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery> 
        : IRequestHandler<TQuery, Unit> 
        where TQuery : IQuery<Unit>
    {
        
    }

    public interface IQueryHandler<in TQuery, TResponse> 
        : IRequestHandler<TQuery, TResponse> 
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
        
    }
}