namespace ServiceXpert.Application.DataObjects
{
    public abstract class DataObjectBase
    {
        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        protected DataObjectBase(bool isSkipCreateDate = false)
        {
            if (!isSkipCreateDate)
            {
                this.CreateDate = DateTime.UtcNow;
            }

            this.ModifyDate = DateTime.UtcNow;
        }
    }
}
