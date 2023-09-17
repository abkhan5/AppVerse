using MediatR;

namespace AppVerse.Infrastructure.Queries;

public interface IQuery : IRequest
{

}
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

public interface IQuery<TRequest, TResponse> : IRequest<TResponse>
{
}

public interface IQueryStream<out TResponse> : IStreamRequest<TResponse>
{
}



public static class MediatorExtensions
{
    
}