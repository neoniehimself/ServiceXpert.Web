namespace ServiceXpert.Web.Models;
public abstract class ModelBase<TId>
{
    protected readonly string basicDateFormat = "MM/dd/yy";
    protected readonly string basicDateFormatWithTime = "MM/dd/yy h:mm:ss tt";

    public TId Id { get; set; } = default!;

    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedDateFormatted => this.CreatedDate.ToString(this.basicDateFormat);

    public string CreatedDateWithTimeFormatted => this.CreatedDate.ToString(this.basicDateFormatWithTime);

    public Guid ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string ModifiedDateFormatted => this.ModifiedDate != null ? this.ModifiedDate.Value.ToString(this.basicDateFormat) : string.Empty;

    public string ModifiedDateWithTimeFormatted => this.ModifiedDate != null ? this.ModifiedDate.Value.ToString(this.basicDateFormatWithTime) : string.Empty;
}

public abstract class ModelBaseForCreate
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}

public abstract class ModelBaseForUpdate
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}