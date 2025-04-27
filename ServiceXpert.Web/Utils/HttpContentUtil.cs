using ServiceXpert.Web.Constants;
using System.Text;
using NewtonsoftJson = Newtonsoft.Json;

namespace ServiceXpert.Web.Utils;
public static class HttpContentUtil
{
    public static StringContent SerializeContentWithApplicationJson(object value)
    {
        return new StringContent(NewtonsoftJson.JsonConvert.SerializeObject(value), Encoding.UTF8,
            HttpMediaType.ApplicationJson);
    }

    public static T? DeserializeContent<T>(HttpResponseMessage response)
    {
        return NewtonsoftJson.JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
    }
}
