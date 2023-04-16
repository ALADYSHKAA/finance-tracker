namespace Application._Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(object obj, string member)
        : base($"{obj.GetType().FullName}.{member} was not found")
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}