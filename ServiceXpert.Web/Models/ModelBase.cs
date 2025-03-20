namespace ServiceXpert.Web.Models
{
    public abstract class ModelBase
    {
        protected readonly string dateFormat = "yy/MM/dd";

        public DateTime CreateDate { get; set; }

        public string CreateDateFormatted
        {
            get
            {
                return this.CreateDate.ToString(this.dateFormat);
            }
        }

        public DateTime ModifyDate { get; set; }

        public string ModifyDateFormatted
        {
            get
            {
                return this.ModifyDate.ToString(this.dateFormat);
            }
        }
    }
}
