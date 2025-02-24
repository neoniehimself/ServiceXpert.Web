using Newtonsoft.Json;

namespace ServiceXpert.API.Application.DataTransferObjects
{
    public abstract class DataObjectBase
    {
        [JsonIgnore]
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public DateTime ModifyDate { get; set; }
    }
}
