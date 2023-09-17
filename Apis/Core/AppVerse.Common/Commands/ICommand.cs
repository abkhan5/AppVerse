using MediatR;

namespace AppVerse.Infrastructure.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<T> : IRequest<T>
{
}