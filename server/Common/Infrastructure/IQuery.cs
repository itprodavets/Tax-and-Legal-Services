using MediatR;

namespace Common.Infrastructure
{
	public interface IQuery<out TResult> : IRequest<TResult>
	{
	}

	public interface IQuery<in TSpecification, out TResult> : IRequest<TResult>
	{
	}
}