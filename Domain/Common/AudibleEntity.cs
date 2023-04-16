using Domain.Domains.Users.Entities;

namespace Domain.Common;

public class AudibleEntity : IAudibleDates
{
    public long? CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public long? UpdatedById { get; set; }
    public User UpdatedBy { get; set; }
    public DateTime? Deleted { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}