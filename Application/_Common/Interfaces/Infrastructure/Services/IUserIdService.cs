namespace Application._Common.Interfaces.Infrastructure.Services;

public interface IUserIdService
{
    public long? GetUserId();

    public long? GetImposerId();

    public string GetSsoUserGuid();

    public string GetSsoUserEmail();
}