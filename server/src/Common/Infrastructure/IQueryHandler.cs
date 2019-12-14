using MediatR;

namespace Common.Infrastructure
{
	public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
	{
	}
}