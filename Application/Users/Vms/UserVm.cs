using Application._Common.Mappings;
using Domain.Domains.Users.Entities;

namespace Application.Users.Vms;

public class UserVm : IMapFrom<User>
{
    public long Id { get; set; }

    public string Nickname { get; set; }

    public long? BotConversationId { get; set; }

    public bool Disabled { get; set; }

    public string FullName { get; set; }
}