namespace ServiceXpert.Application.DataObjects
{
    public abstract class DataObjectBase
    {
        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
