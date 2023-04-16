using Domain.Common;
using Domain.Domains.Users.Enums;

namespace Domain.Domains.Users.Entities;

public class User : IAudibleDates
{
    public long Id { get; set; }

    public string Email { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Nickname { get; set; }

    public Guid? ExternalIdentityProviderGuid { get; set; }

    public ExternalProviderTypes? ExternalIdentityProviderType { get; set; }

    public bool Disabled { get; set; }

    public string FullName => FirstName + " " + LastName;

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}