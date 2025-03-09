namespace ServiceXpert.Web.Models
{
    public abstract class ModelBase
    {
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        public DateTime ModifyDate { get; } = DateTime.UtcNow;
    }
}
