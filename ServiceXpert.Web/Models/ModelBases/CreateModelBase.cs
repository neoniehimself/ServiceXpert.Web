namespace ServiceXpert.Web.Models.ModelBases;

public abstract class CreateModelBase
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
