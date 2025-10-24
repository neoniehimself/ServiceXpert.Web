namespace ServiceXpert.Web.Models;
public abstract class UpdateModelBase
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
