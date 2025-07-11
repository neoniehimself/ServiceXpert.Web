namespace ServiceXpert.Web.Models;
public abstract class ModelBase
{
    protected readonly string dateFormat = "dd/MM/yy";
    protected readonly string dateTimeFormat = "dd/MM/yy h:mm:ss tt";

    public DateTime? CreateDate { get; set; }

    public string CreateDateFormatted => this.CreateDate != null
        ? this.CreateDate.Value.ToString(this.dateFormat)
        : string.Empty;

    public string CreateDateTimeFormatted => this.CreateDate != null
        ? this.CreateDate.Value.ToString(this.dateTimeFormat)
        : string.Empty;

    public DateTime? ModifyDate { get; set; }

    public string ModifyDateFormatted => this.ModifyDate != null
        ? this.ModifyDate.Value.ToString(this.dateFormat)
        : string.Empty;

    public string ModifyDateTimeFormatted => this.ModifyDate != null
        ? this.ModifyDate.Value.ToString(this.dateTimeFormat)
        : string.Empty;
}
