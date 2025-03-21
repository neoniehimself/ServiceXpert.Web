using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceXpert.Domain.Entities
{
    public abstract partial class EntityBase
    {
        protected readonly string dateFormat = "yy/MM/dd";

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }

    public abstract partial class EntityBase
    {
        [NotMapped]
        public string CreateDateFormatted => this.CreateDate.ToString(this.dateFormat);
        [NotMapped]
        public string ModifyDateFormatted => this.ModifyDate.ToString(this.dateFormat);
    }
}
