using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application._Common.Behaviors;

public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;


    public RequestPerformanceBehaviour(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 1000)
        {
            var name = typeof(TRequest).Name;
            _logger.LogWarning(
                $"Tender Bot Long Running Request: {name} ({_timer.ElapsedMilliseconds} milliseconds) {request}");
        }

        return response;
    }
}