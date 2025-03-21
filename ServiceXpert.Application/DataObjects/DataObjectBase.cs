namespace ServiceXpert.Application.DataObjects
{
    public abstract class DataObjectBase
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
    }
}
