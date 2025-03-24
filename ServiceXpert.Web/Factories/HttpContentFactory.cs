using ServiceXpert.Web.Helpers;
using System.Text;
using NewtonsoftJson = Newtonsoft.Json;

namespace ServiceXpert.Web.Factories
{
    public static class HttpContentFactory
    {
        public static StringContent SerializeContent(object value)
        {
            var serializedObject = NewtonsoftJson.JsonConvert.SerializeObject(value);
            return new StringContent(serializedObject, Encoding.UTF8, HttpMediaType.ApplicationJson);
        }
    }
}
