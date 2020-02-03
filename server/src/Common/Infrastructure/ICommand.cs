using MediatR;

namespace Common.Infrastructure
{
	public interface ICommand : IRequest
	{
	}

	public interface ICommand<out TResult> : IRequest<TResult>
	{
	}
}