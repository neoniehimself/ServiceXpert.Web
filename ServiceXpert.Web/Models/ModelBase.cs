namespace ServiceXpert.Web.Models;
public abstract class ModelBase<TId>
{
    public TId Id { get; set; } = default!;
}
