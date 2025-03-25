using ServiceXpert.Web.Helpers;
using System.Text;
using NewtonsoftJson = Newtonsoft.Json;

namespace ServiceXpert.Web.Factories
{
    public static class HttpContentFactory
    {
        public static StringContent SerializeContent(object value)
        {
            return new StringContent(NewtonsoftJson.JsonConvert.SerializeObject(value), Encoding.UTF8, HttpMediaType.ApplicationJson);
        }

        public static T? DeserializeContent<T>(HttpResponseMessage response)
        {
            return NewtonsoftJson.JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
