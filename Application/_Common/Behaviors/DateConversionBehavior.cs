using FluentValidation;
using MediatR;

namespace Application._Common.Behaviors;

public class DateConversionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public DateConversionBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        var dateValidator = new DateTimeValidator<TRequest>();
        var context = new ValidationContext<TRequest>(request);
        var k = await dateValidator.ValidateAsync(context, cancellationToken);

        if (k.Errors.Any(x => x != null)) throw new ValidationException(k.Errors);

        var dateTimeRequestProperties = request.GetType()
            .GetProperties()
            .Where(x => (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?)) &&
                        (DateTime?) x.GetValue(request) != null);

        foreach (var propertyInfo in dateTimeRequestProperties)
        {
            var dateTime = (DateTime?) propertyInfo.GetValue(request);
            if (dateTime is not null && dateTime?.Kind != DateTimeKind.Utc)
                propertyInfo.SetValue(request, dateTime?.ToUniversalTime());
        }

        return await next();
    }
}

public class DateTimeValidator<T> : AbstractValidator<T>
{
    public DateTimeValidator()
    {
        RuleForEach(request =>
                request.GetType()
                    .GetProperties()
                    .Where(x => (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?)) &&
                                (DateTime?) x.GetValue(request) != null))
            .Must((obj, x) => { return ((DateTime?) x.GetValue(obj))?.Kind != DateTimeKind.Unspecified; })
            .OverridePropertyName("Валидация дат на указание типа даты. ")
            .WithMessage((request, prop) =>
                $"В запросе {request.GetType().Name} содержится дата в не верном формате {prop.Name}");
    }
}