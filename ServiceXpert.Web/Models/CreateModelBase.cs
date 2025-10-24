namespace ServiceXpert.Web.Models;
public abstract class CreateModelBase
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
