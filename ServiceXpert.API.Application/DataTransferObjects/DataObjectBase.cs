namespace ServiceXpert.Api.Application.DataTransferObjects
{
    public abstract class DataObjectBase
    {
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        public DateTime ModifyDate { get; } = DateTime.UtcNow;
    }
}
