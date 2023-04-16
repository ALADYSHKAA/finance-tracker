using System.Runtime.Serialization;
using System.Text;
using FluentValidation.Results;
using LinqKit;

namespace Application._Common.Exceptions;

[Serializable]
public class TenderBotValidationException : Exception
{
    public TenderBotValidationException()
        : base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
    }

    public TenderBotValidationException(List<ValidationFailure> failures)
        : base(GenerateErrorMessage(failures))
    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

    private static string GenerateErrorMessage(List<ValidationFailure> failures)
    {
        var message = new StringBuilder(failures.Count > 1 ? "Ошибки" : "Ошибка");
        message
            .Append(" валидации:")
            .Append(failures.Count > 1 ? Environment.NewLine : "");


        var failuresGrouped = failures.GroupBy(x => x.PropertyName);

        failuresGrouped.ForEach(x =>
            message.Append(x.Key)
                .Append(": ")
                .Append(string.Join(Environment.NewLine, x.Select(vg => vg.ErrorMessage)))
                .Append(Environment.NewLine));
        return message.ToString();
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null) throw new ArgumentNullException("info");

        info.AddValue("errors", Failures);
        info.AddValue("message", Message);
        base.GetObjectData(info, context);
    }
}