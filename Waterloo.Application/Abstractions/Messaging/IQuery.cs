using MediatR;
using Waterloo.SharedKernel;

namespace Waterloo.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
