namespace ServiceXpert.Web.Models;
public abstract class ModelBase
{
    protected readonly string dateFormat = "dd/MM/yy";

    public DateTime CreateDate { get; set; }

    public string CreateDateFormatted => this.CreateDate.ToString(this.dateFormat);

    public DateTime? ModifyDate { get; set; }

    public string ModifyDateFormatted => this.ModifyDate != null
        ? this.ModifyDate.Value.ToString(this.dateFormat)
        : string.Empty;
}
