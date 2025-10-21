namespace ServiceXpert.Web.Models.ModelBases;
public abstract class ModelBase<TId>
{
    public TId Id { get; set; } = default!;
}
