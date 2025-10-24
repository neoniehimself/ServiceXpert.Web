namespace ServiceXpert.Web.Models;
public class AuditableModelBase<TId> : ModelBase<TId>
{
    protected readonly string basicDateFormat = "MM/dd/yy";
    protected readonly string basicDateFormatWithTime = "MM/dd/yy h:mm:ss tt";

    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string CreatedDateFormatted => this.CreatedDate.ToString(this.basicDateFormat);

    public string CreatedDateWithTimeFormatted => this.CreatedDate.ToString(this.basicDateFormatWithTime);

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public string ModifiedDateFormatted => this.ModifiedDate != null ? this.ModifiedDate.Value.ToString(this.basicDateFormat) : string.Empty;

    public string ModifiedDateWithTimeFormatted => this.ModifiedDate != null ? this.ModifiedDate.Value.ToString(this.basicDateFormatWithTime) : string.Empty;
}
